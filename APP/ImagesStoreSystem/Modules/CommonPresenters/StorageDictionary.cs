using System.Collections.Generic;
using System.Collections.ObjectModel;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;

namespace CommonPresenters
{
	public abstract class StorageDictionary
	{
        public static ObservableCollection<StationPresenter> Stations(IDataAccessObjects dao)
		{
			IList<Station> stations = dao.ReadAll<Station>();
			ObservableCollection<StationPresenter> res = new ObservableCollection<StationPresenter>();
			foreach (Station station in stations)
			{
				res.Add(new StationPresenter(station));
			}
			return res;
		}

        public static ObservableCollection<SatellitePresenter> Satellites(IDataAccessObjects dao)
		{
			IList<Satellite> satellites = dao.ReadAll<Satellite>();
			ObservableCollection<SatellitePresenter> res = new ObservableCollection<SatellitePresenter>();
			foreach (Satellite satellite in satellites)
			{
				res.Add(new SatellitePresenter(satellite));
			}
			return res;
		}

		public static ObservableCollection<ImageLevelPresenter> Levels(IDataAccessObjects dao)
		{
			IList<ImageLevel> imageLevels = dao.ReadAll<ImageLevel>();
			ObservableCollection<ImageLevelPresenter> res = new ObservableCollection<ImageLevelPresenter>();
			foreach (ImageLevel satellite in imageLevels)
			{
				res.Add(new ImageLevelPresenter(satellite));
			}
			return res;
		}

		public static ObservableCollection<SensorPresenter> Sensors(Satellite satellite)
		{
			if (satellite == null) return null;

			ObservableCollection<SensorPresenter> res = new ObservableCollection<SensorPresenter>();
			foreach (SatelliteSensor sensor in satellite.Sensors)
			{
				res.Add(new SensorPresenter(sensor));
			}
			return res;
		}

		public static ObservableCollection<SensorTypePresenter> SensorTypes(IDataAccessObjects dao)
		{
			IList<SatelliteSensorType> sensorTypes = dao.ReadAll<SatelliteSensorType>();
			ObservableCollection<SensorTypePresenter> res = new ObservableCollection<SensorTypePresenter>();
			foreach (SatelliteSensorType sensorType in sensorTypes)
			{
				res.Add(new SensorTypePresenter(sensorType));
			}
			return res;
		}

		public static ObservableCollection<ReceiveSession> ReceiveSessions(IDataAccessObjects dao)
		{
			IList<ReceiveSession> receiveSessions = dao.ReadAll<ReceiveSession>();
			return new ObservableCollection<ReceiveSession>(receiveSessions);
		}

		public static ObservableCollection<SurveySensor> SurveySensors(IDataAccessObjects dao)
		{
			IList<SurveySensor> surveySensors = dao.ReadAll<SurveySensor>();
			return new ObservableCollection<SurveySensor>(surveySensors);
		}

	}
}
