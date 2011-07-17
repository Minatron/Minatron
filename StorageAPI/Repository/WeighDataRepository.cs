using System.Collections.Generic;
using Band.Storage.Core;

namespace Band.Storage.Minatron
{
    public class WeighDataRepository : Band.Storage.RepositoryBase<WeighData>
    {
        public WeighDataRepository(ISessionCreator factory)
            : base(factory)
        { }

        public IList<WeighData> GetAll(IEnumerable<IStorageFilter> filters)
        {
            return _factory.Function(() => Core.GetAll.WeighData(GetNewSession(), filters));
        }

    }
}
