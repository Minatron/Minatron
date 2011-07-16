using CommonObjects;
using ImagesStoreSystem.DBProvider.Core;
using LocalizationLibrary;
using Microsoft.Practices.Composite.Events;

namespace ReceivePlanModule.Presenters
{
	class PackIdentityFilterPresenter : FilterPresenter<IReceivePlanTaskFilter>
	{
		long _packetID = 0;

		public override string Name
		{
			get
			{
                return Lang.GetTitle("Modules/ReceivePlan/FiltersDescription/PacketId");
			}
		}

		public long PacketID
		{
			get
			{
				return _packetID;
			}
			set
			{
				_packetID = value;
				OnPropertyChanged("PacketID");
			}
		}

		public override IReceivePlanTaskFilter[] Filters
		{
			get
			{
				return new IReceivePlanTaskFilter[] { new ReceivePlanPackIdentityFilter(_packetID) };
			}
		}

		public override void Reset() { }

		public PackIdentityFilterPresenter(IEventAggregator eventAggragator)
			: base(eventAggragator) { }
	}
}
