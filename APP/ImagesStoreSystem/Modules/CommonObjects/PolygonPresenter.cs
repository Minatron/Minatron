using MapWraper.Events;
using Microsoft.Practices.Composite.Events;
using Microsoft.SqlServer.Types;

namespace CommonObjects
{
	public class PolygonPresenter : Presenter
	{
		protected double _bottomLatitude = 0;
		protected double _topLatitude = 0;
		protected double _leftLongitude = 0;
		protected double _rightLongitude = 0;

		protected string _mapName = null;
		protected SqlGeography _polygon = null;

		public virtual double BottomLatitude
		{
			get
			{
				return _bottomLatitude;
			}
		}

		public virtual double TopLatitude
		{
			get
			{
				return _topLatitude;
			}
		}

		public virtual double LeftLongitude
		{
			get
			{
				return _leftLongitude;
			}
		}

		public virtual double RightLongitude
		{
			get
			{
				return _rightLongitude;
			}
		}

		public bool HasPolygon
		{
			get
			{
				return _polygon != null;
			}
		}

		public virtual SqlGeography Polygon
		{
			get
			{
				return _polygon;
			}
			set
			{
				_polygon = value;
				FillFromPolygon(_polygon);
				_eventAggregator.GetEvent<DrawObjectEvent>().Publish(new MapObjectArgs(_mapName, _polygon));
				OnPropertyChanged("Polygon");
				OnPropertyChanged("HasPolygon");
			}
		}

		protected void FillFromPolygon(SqlGeography polygon)
		{
			if (polygon != null && polygon.STNumPoints().Value == 5)
			{
				var p1 = polygon.STPointN(1);
				var p2 = polygon.STPointN(3);
				if (p1 != null && p2 != null && !p1.IsNull && !p2.IsNull)
				{
					_topLatitude = p1.Lat.Value;
					_bottomLatitude = p2.Lat.Value;
					_leftLongitude = p1.Long.Value;
					_rightLongitude = p2.Long.Value;

					OnPropertyChanged("TopLatitude");
					OnPropertyChanged("BottomLatitude");
					OnPropertyChanged("LeftLongitude");
					OnPropertyChanged("RightLongitude");
				}
			}
			else
			{
				_topLatitude = 0;
				_bottomLatitude = 0;
				_leftLongitude = 0;
				_rightLongitude = 0;
			}
		}

		public PolygonPresenter(IEventAggregator eventAggregator, string mapName = null)
			: base(eventAggregator)
		{
			_mapName = mapName;
			if (mapName != null)
			{
				_eventAggregator.GetEvent<ObjectChangedEvent>().Subscribe(
				   arg =>
				   {
					   if (arg.Name.Equals(_mapName))
					   {
						   Polygon = arg.Geography;
					   }
				   }, true);
			}
		}
	}
}
