using System.Collections.Generic;
using Band.Storage.Core;
using Band.Storage.Minatron.Data;

namespace Band.Storage.Minatron.Repository
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
