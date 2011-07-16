using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping.Data
{
    public sealed class ReceivePlanTaskMap : ClassMap<ReceivePlanTask>
    {
        public ReceivePlanTaskMap()
        {
            Table("RecivePlainTasks");

            Id(x => x.Id, "RPlainID")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.PackIdentity, "RPlainPackIdentity")
                .Not.Nullable();

            Map(x => x.StationCatalogNumber, "St_Number")
                .Not.Nullable();

            Map(x => x.SatelliteCatalogNumber, "Sat_Number")
                .Not.Nullable();

            Map(x => x.StartTime, "RPlainStartTime")
                .Default("Sysutcdatetime()")
                .CustomType("DateTime2")
                .Not.Nullable();

            Map(x => x.EndTime, "RPlainEndTime")
                .Default("Sysutcdatetime()")
                .CustomType("DateTime2")
                .Not.Nullable();

            Map(x => x.Aborted, "RPlainAborted")
                .Default("0")
                .Not.Nullable();

            References(x => x.ResultSession, "RSession_ID")
               .ForeignKey("FK_RPlain_RSession")
               .Cascade.SaveUpdate()
               .Nullable();

            Map(x => x.Body, "RPlainBody")
                .LazyLoad()
                .CustomSqlType("nvarchar(max)")
                .CustomType("StringClob")
                .Nullable();


        }
    }
}
