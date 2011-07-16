using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class SatelliteSensorMap : ClassMap<SatelliteSensor>
    {
        public SatelliteSensorMap()
        {
            Table("Sensors");

            Id(x => x.Id, "SenID")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Title, "SenTitle")
                .Not.Nullable()
                .Length(25)
                .Unique();
        }
    }
}
