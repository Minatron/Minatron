using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class AttributeTitleMap : ClassMap<AttributeTitle>
    {
        public AttributeTitleMap()
        {
            Table("AttributeType");

            Id(x => x.Id, "AttTypeID")                
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Title, "AttTitle")                
                .Not.Nullable()                
                .Length(250)
                .Unique();
        }
    }
}
