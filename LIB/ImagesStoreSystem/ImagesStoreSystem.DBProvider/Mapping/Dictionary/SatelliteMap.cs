using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class SatelliteMap : ClassMap<Satellite>
    {
        public SatelliteMap()
        {
            Table("Satellites");

            Id(x => x.Id, "SatID")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Title, "SatTitle")
                .Not.Nullable()
                .Length(25)
                .Unique();

            Map(x => x.CatalogNumber, "SatNumber")
                .Not.Nullable()
                .Unique();
        }
    }
}
