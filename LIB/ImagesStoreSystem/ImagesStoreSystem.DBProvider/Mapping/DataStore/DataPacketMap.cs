using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class DataPacketMap : ClassMap<DataPacket>
    {
        public DataPacketMap()
        {
            Table("FileGroups");

            Id(x => x.Id, "FGroupID")
                .Not.Nullable()
                .GeneratedBy.Identity();

            HasMany(x => x.Files)
                .ForeignKeyCascadeOnDelete()
                .KeyColumn("FGroup_ID")
                .Inverse()
                .Cascade.All();
        }
    }
}
