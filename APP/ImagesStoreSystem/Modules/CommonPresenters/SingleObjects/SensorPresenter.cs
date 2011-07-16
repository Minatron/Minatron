using System;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;

namespace CommonPresenters
{
	public class SensorPresenter : Presenter
	{
		SatelliteSensor m_sensor;


		public long ID
		{
			get
			{
				return m_sensor.Id;
			}
		}

		public string Title
		{
			get
			{
				return m_sensor.Title;
			}
			set
			{
				m_sensor.Title = value;
				OnPropertyChanged("Title");
			}
		}

		public SensorTypePresenter Type
		{
			get
			{
				return new SensorTypePresenter(m_sensor.Type);
			}
			set
			{
				m_sensor.Type = (SatelliteSensorType)value;
				OnPropertyChanged("SensorTypePresenter");
			}
		}

		public SensorPresenter(SatelliteSensor sensor)
		{
			if (sensor == null) throw new NullReferenceException("sensor");
			m_sensor = sensor;
		}

		public static explicit operator SatelliteSensor(SensorPresenter presenter)
		{
			return presenter.m_sensor;
		}
	}
}
