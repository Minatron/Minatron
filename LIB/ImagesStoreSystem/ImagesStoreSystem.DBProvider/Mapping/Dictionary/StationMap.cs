using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class StationMap : ClassMap<Station>
    {
        public StationMap()
        {
            Table("Stations");

            Id(x => x.Id, "StID")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Title, "StTitle")
                .Not.Nullable()
                .Length(25)
                .Unique();

            Map(x => x.CatalogNumber, "StNumber")
               .Not.Nullable()
               .Unique();
        }
    }
}
