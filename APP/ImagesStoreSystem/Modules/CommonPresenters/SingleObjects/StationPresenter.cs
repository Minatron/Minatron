using System;
using ImagesStoreSystem.DBProvider.Core;
using StorageModule.Services;

namespace CommonPresenters
{
	public class StationPresenter : Presenter
	{
		Station m_station;

		public long ID
		{
			get
			{
				return m_station.Id;
			}
		}

		public String Title
		{
			get
			{
				return m_station.Title;
			}
			set
			{
				m_station.Title = value;
				OnPropertyChanged("Title");
			}
		}

		public StationPresenter(Station station)
		{
			if (station == null) throw new NullReferenceException("station");
			m_station = station;
		}

		public static explicit operator Station(StationPresenter presenter)
		{
			return presenter.m_station;
		}
	}
}
