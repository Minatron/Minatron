using Band.Storage.Minatron.Data;
using FluentNHibernate.Mapping;

namespace Band.Storage.Minatron.Mapping
{
    public sealed class WeighDataMap : ClassMap<WeighData>
    {
        public WeighDataMap()
        {
            Table("WeighData");

            Id(x => x.Id, "ID")
               .Not.Nullable()
               .GeneratedBy.Identity();

            Map(x => x.Course, "CourseID")
               .CustomType<int>()
               .Not.Nullable();

            Map(x => x.WeighTime, "WeighTime")
               .Default("Sysutcdatetime()")
               .CustomType("DateTime2")
               .Not.Nullable();

            Map(x => x.Weigh, "Weigh")
              .Default("0")
              .Not.Nullable();

            Map(x => x.AvgSpeed, "AvgSpeed")
              .Default("0")
              .Not.Nullable();           
        }
    }
}
