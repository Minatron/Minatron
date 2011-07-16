using System;
using StorageModule.Services;

namespace StorageModule.Model
{
    public enum ContentTransferStatus
    {        
        Progress,
        Ok,
        Error
    }


	public class ContentTransferWatcher 
	{
		#region sync

		volatile object _asyncRoot = new object();

		#endregion

		long _totalBytes;
		long _bytesPassed = 0;
		bool _isActive = false;
        ContentTransferStatus _status = ContentTransferStatus.Ok;
		DateTime _startTime;

		Func<long> _getSpeed;
		Action<long> _setSpeed;

		ContentTransferService _transferService;

		internal ContentTransferWatcher(ContentTransferService transferService)
		{
			_transferService = transferService;
		}

		#region Writers

		internal void Elapsed(long passedBytes)
		{
			lock (_asyncRoot)
			{
				double totalSeconds = TimePassed.TotalSeconds;
				if (totalSeconds > 0)
				{
					Speed = (long)(passedBytes / totalSeconds);
				}

				_bytesPassed = passedBytes;
			}
		}

		internal void Start(long totalBytes, OperationType type)
		{
			lock (_asyncRoot)
			{

				_getSpeed = (type == OperationType.Download) ? (Func<long>)_transferService.GetDownloadSpeed : _transferService.GetUploadSpeed;
				_setSpeed = (type == OperationType.Download) ? (Action<long>)_transferService.SetDownloadSpeed : _transferService.SetUploadSpeed;

				_isActive = true;
				_startTime = DateTime.UtcNow;
				_totalBytes = totalBytes;
				_bytesPassed = 0;
                _status = ContentTransferStatus.Progress;
			}
		}

		internal void End(Exception ex=null)
		{
			lock (_asyncRoot)
			{
				_isActive = false;
				_bytesPassed = TotalBytes;
                _status = (ex == null) ? ContentTransferStatus.Ok : ContentTransferStatus.Error;
			}
		}

		#endregion

		public long TotalBytes
		{
			get
			{
				lock (_asyncRoot)
				{
					return _totalBytes;
				}
			}
		}

		public bool IsActive
		{
			get
			{
				lock (_asyncRoot)
				{
					return _isActive;
				}
			}
		}

        public ContentTransferStatus Status
        {
            get
            {
                lock (_asyncRoot)
                {
                    return _status;
                }
            }
        }

		public long Speed
		{
			get
			{
				lock (_asyncRoot)
				{
					if (!_isActive) return 0;
					return _getSpeed();
				}
			}
			protected set
			{
				lock (_asyncRoot)
				{
					if (_isActive) _setSpeed(value);
				}
			}
		}

		public TimeSpan TimePassed
		{
			get
			{
				lock (_asyncRoot)
				{
					return DateTime.UtcNow - _startTime;
				}
			}
		}

		public TimeSpan? TimeLeft
		{
			get
			{
				lock (_asyncRoot)
				{
					double speed = Speed;
					if (speed > 0)
					{
						var bytesLeft = Math.Max(0, TotalBytes - _bytesPassed);
						return TimeSpan.FromSeconds(bytesLeft / speed);
					}
					else
					{
						return null;
					}
				}
			}
		}

		public long PassedBytes
		{
			get
			{
				lock (_asyncRoot)
				{
					_bytesPassed = Math.Min(TotalBytes, Math.Max(_bytesPassed, (long)(Speed * TimePassed.TotalSeconds)));
					return _bytesPassed;
				}
			}
		}
	}
}
