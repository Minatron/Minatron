using System.Collections.Generic;
using ImagesStoreSystem.DBProvider.Core.Extension;

namespace ImagesStoreSystem.DBProvider.Core
{
	public class ImageRepository : UpdatableRepository<Image>
    {
        public ImageRepository(ISessionCreator factory)
            : base(factory)
        { }

        public IList<Image> GetAll(IEnumerable<IImageFilter> filters)
        {
           return _factory.Function(() => Extension.GetAll.Images(GetNewSession(), filters));
        }

        
    }
}
