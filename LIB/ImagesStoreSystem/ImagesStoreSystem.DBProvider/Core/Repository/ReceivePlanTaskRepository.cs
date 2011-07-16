using System.Collections.Generic;
using ImagesStoreSystem.DBProvider.Core.Extension;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class ReceivePlanTaskRepository : UpdatableRepository<ReceivePlanTask>
    {
        public ReceivePlanTaskRepository(ISessionCreator factory)
            : base(factory)
        { }

        public IList<ReceivePlanTask> GetAll(IEnumerable<IReceivePlanTaskFilter> filters)
        {
            return _factory.Function(() => Extension.GetAll.ReceivePlanTasks(GetNewSession(), filters));
        }


    }
}
