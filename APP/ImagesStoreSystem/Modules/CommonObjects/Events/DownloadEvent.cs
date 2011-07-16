using System.Collections.Generic;
using ImagesStoreSystem.DBProvider.Core;
using Microsoft.Practices.Composite.Presentation.Events;
using System;

namespace CommonObjects
{
	public class DownloadEvent : CompositePresentationEvent<DownloadEventArgs>
	{
	}

	public class DownloadEventArgs
	{
		public IEnumerable<DataFile> Files { get; protected set; }
		public Action OnDownloadHappend { get; protected set; }

		public DownloadEventArgs(IEnumerable<DataFile> files, Action onDownloadHappend = null)
		{
			if (onDownloadHappend == null)
			{
				onDownloadHappend = () => { };
			}
			if (files == null)
			{
				files = new DataFile[0];
			}
			OnDownloadHappend = onDownloadHappend;
			Files = files;
		}
	}
}
