using System.Windows;
using System.Windows.Input;
using ImagesStoreSystem.DBProvider;
using MapWraper.Events;
using Microsoft.Practices.Composite.Events;
using Microsoft.SqlServer.Types;
using WPF.Patterns.Commands;
using System.Windows.Controls.Primitives;

namespace CommonObjects
{
	public class PolygonSelector : PolygonPresenter
	{
		bool _isIncorrectCoorinates = false;

		public bool IsIncorrectCoordinates
		{
			get
			{
				return _isIncorrectCoorinates;
			}
			protected set
			{
				_isIncorrectCoorinates = value;
				OnPropertyChanged("IsIncorrectCoordinates");
			}
		}

		public ICommand RecreateCommand { get; protected set; }

		public new double BottomLatitude
		{
			get
			{
				return base.BottomLatitude;
			}
			set
			{
				_bottomLatitude = value;
				OnPropertyChanged("BottomLatitude");
			}
		}

		public new double TopLatitude
		{
			get
			{
				return base.TopLatitude;
			}
			set
			{
				_topLatitude = value;
				OnPropertyChanged("TopLatitude");
			}
		}

		public new double LeftLongitude
		{
			get
			{
				return base.LeftLongitude;
			}
			set
			{
				_leftLongitude = value;
				OnPropertyChanged("LeftLongitude");
			}
		}

		public new double RightLongitude
		{
			get
			{
				return base.RightLongitude;
			}
			set
			{
				_rightLongitude = value;
				OnPropertyChanged("RightLongitude");
			}
		}

		public override SqlGeography Polygon
		{
			get
			{
				return base.Polygon;
			}
			set
			{
				ResetCoordinates();
				base.Polygon = value;
				IsIncorrectCoordinates = false;
			}
		}

		void ResetCoordinates()
		{
			LeftLongitude = 0;
			RightLongitude = 0;
			TopLatitude = 0;
			BottomLatitude = 0;
		}

		public void RecreatePolygon()
		{
			try
			{
				_polygon = GeographyConverter.PointsToGeography(
						new Point(_leftLongitude, _topLatitude),
						new Point(_rightLongitude, _topLatitude),
						new Point(_rightLongitude, _bottomLatitude),
						new Point(_leftLongitude, _bottomLatitude),
						new Point(_leftLongitude, _topLatitude)
					);
				IsIncorrectCoordinates = false;
				HasProblems = false;
			}
			catch (System.FormatException)
			{
				IsIncorrectCoordinates = true;
				HasProblems = true;
				_polygon = null;
			}
			catch (System.Exception)
			{
				IsIncorrectCoordinates = true;
				_polygon = null;
				HasProblems = true;
				//здесь иногда срабатывает GLArgumentException, который не понятно как отловить
				//например при вводе полигона (-50 -50) - (80 80)
				//throw ex; 
			}
			finally
			{
				OnPropertyChanged("HasPolygon");
				OnPropertyChanged("Polygon");
				if (_mapName != null)
				{
					_eventAggregator.GetEvent<DrawObjectEvent>().Publish(new MapObjectArgs(_mapName, _polygon));
				}
			}
		}

		public PolygonSelector(IEventAggregator eventAggregator, string mapName = null)
			: base(eventAggregator, mapName)
		{
			RecreateCommand = new DelegateCommand<object>(
				arg =>
				{
					RecreatePolygon();
					if (arg is ToggleButton)
					{
						((ToggleButton)arg).IsChecked = HasProblems;
					}
				});
		}
	}
}
