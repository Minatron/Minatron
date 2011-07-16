using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;
namespace ImagesStoreSystem.DBProvider.Mapping
{


    public sealed class ReciveSessionMap : ClassMap<ReceiveSession>
    {
        public ReciveSessionMap()
        {
            Table("ReciveSession");

            Id(x => x.Id, "RSessionID")
                .Not.Nullable()
                .GeneratedBy.Identity();

			Map(x => x.SatelliteCatalogNumber, "Sat_Number")
				.Not.Nullable();

			Map(x => x.StationCatalogNumber, "St_Number")
				.Not.Nullable();

            Map(x => x.StartTime, "RSessionStartTime")
                .Default("Sysutcdatetime()")
                .CustomType("DateTime2")
                .Not.Nullable();

            Map(x => x.EndTime, "RSessionEndTime")
                .Default("Sysutcdatetime()")
                .CustomType("DateTime2")
                .Not.Nullable();

            Map(x => x.Coor, "RSessionCoor")
                .CustomType(typeof(SqlGeographyUserType))
                .CustomSqlType("geography")
                .Nullable();

            Map(x => x.PacketID, "DATA_ID")
               .Nullable();

            HasMany(x => x.Images)
                //.Not.LazyLoad()
                .KeyColumn("RSession_ID")
                .Inverse()
                .Cascade.SaveUpdate();
        }
    }

}
