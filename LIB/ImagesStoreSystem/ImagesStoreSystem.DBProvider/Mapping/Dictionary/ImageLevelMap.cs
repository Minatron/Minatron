using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{

    public sealed class ImageLevelMap : ClassMap<ImageLevel>
    {
        public ImageLevelMap()
        {
            Table("ImageLevels");

            Id(x => x.Id, "ILevelID")                
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Title, "ILevelTitle")                
                .Not.Nullable()                
                .Length(25)
                .Unique();
        }
    }
}
