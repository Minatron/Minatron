using System;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;

namespace CommonPresenters
{
	public class SensorTypePresenter : Presenter
	{
		SatelliteSensorType m_sensorType;

		public String Title
		{
			get
			{
				return m_sensorType.Title;
			}
			set
			{
				m_sensorType.Title = value;
				OnPropertyChanged("Title");
			}
		}

		public SensorTypePresenter(SatelliteSensorType sensorType)
		{
			if (sensorType == null) throw new NullReferenceException("sensorType");
			m_sensorType = sensorType;
		}

		public static explicit operator SatelliteSensorType(SensorTypePresenter sensorType)
		{
			return sensorType.m_sensorType;
		}
	}
}
