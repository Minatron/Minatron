using System;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;

namespace CommonPresenters
{
	public class SatellitePresenter : Presenter
	{
		#region Переменные

		Satellite m_satellite;

		#endregion Переменные



		#region Свойства

		public long ID
		{
			get
			{
				return m_satellite.Id;
			}
		}

		public String Title
		{
			get
			{
				return m_satellite.Title;
			}
			set
			{
				m_satellite.Title = value;
				OnPropertyChanged("Title");
			}
		}

		public String Number
		{
			get
			{
				return m_satellite.CatalogNumber;
			}
			set
			{
				m_satellite.CatalogNumber = value;
				OnPropertyChanged("Number");
			}
		}

		#endregion Свойства



		#region Конструкторы

		public SatellitePresenter(Satellite satellite)
		{
			if (satellite == null) throw new NullReferenceException("satellite");
			m_satellite = satellite;
			//TODO добавить список сенсоров
			//m_sensors = new SensorsListPresenter(m_satellite, storage);
		}

		#endregion Конструкторы


		public static explicit operator Satellite(SatellitePresenter presenter)
		{
			if (presenter == null)
				return null;

			return presenter.m_satellite;
		}
	}
}
