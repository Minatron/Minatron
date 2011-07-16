using System.Collections.Generic;
using ImagesStoreSystem.DBProvider.Core.Extension;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class ReceiveSessionRepository : UpdatableRepository<ReceiveSession>
    {
        public ReceiveSessionRepository(ISessionCreator factory)
            : base(factory)
        { }

        public IList<ReceiveSession> GetAll(IEnumerable<IReceiveSessionFilter> filters)
        {
            return _factory.Function(() => Extension.GetAll.ReceiveSessions(GetNewSession(), filters));
        }
    }
}
