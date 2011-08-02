using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Band.Storage;
using Band.Storage.Minatron;

namespace Band.OLD
{
    public class WeightFilterPresenter : FilterPresenter
    {
        float _min = 0;

		public float MinWeight 
		{ 
			get
			{
				return _min;
			}
			set
			{
				_min = Math.Max(0, value);
				OnPropertyChanged("MinWeight");
			}
		}

		public override string Name
		{
			get
			{
                return @"Вес";
			}
		}

        public override IStorageFilter[] Filters
        {
            get
            {
                return new IStorageFilter[] { new WeightFilter(MinWeight)};
            }
        }

		public override void Reset()
		{
			MinWeight = 0;
		}

        public WeightFilterPresenter()
		{
			Reset();
		}
    }
}
