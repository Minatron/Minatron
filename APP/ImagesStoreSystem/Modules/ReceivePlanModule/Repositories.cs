using ImagesStoreSystem.DBProvider.Core;
using ImageStoreSystem.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using StorageModule.Services;

namespace ReceivePlanModule
{
	static class Repositories
	{
		public static NumberedDictionaryRepository<Station> StationRepo { get; private set; }
		public static NumberedDictionaryRepository<Satellite> SatelliteRepo { get; private set; }

		public static void Init(StorageService storage, IEventAggregator eventAggregator)
		{
			StationRepo = storage.CreateStationRepository();
			SatelliteRepo = storage.CreateSatelliteRepository();

			eventAggregator.GetEvent<RefreshAllEvent>().Subscribe(
			arg =>
			{
				try
				{
					StationRepo.GetAll();
					SatelliteRepo.GetAll();
				}
				catch (ConnectException)
				{
					if (eventAggregator != null)
					{
						eventAggregator.GetEvent<DisconnectEvent>().Publish(null);
					}
				}
			}, ThreadOption.UIThread, true);
		}
	}
}
