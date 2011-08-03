using System.Collections.Generic;
using Band.Storage;

namespace Band.OLD
{
	public interface IFiltersManager
	{
		bool WasChanged { get; }
        IList<IStorageFilter> RecreateActiveFilters();
        IList<IStorageFilter> Filters { get; }
	}
}
