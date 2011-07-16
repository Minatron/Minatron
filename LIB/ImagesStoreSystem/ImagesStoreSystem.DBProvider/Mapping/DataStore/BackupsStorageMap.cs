using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class BackupsStorageMap : ClassMap<BackupsStorage>
    {
        public BackupsStorageMap()
        {
            Table("RemovableStorages");

            Id(x => x.Id, "RStorageID")                
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Title, "RStorageTitle")                
                .Not.Nullable()                
                .Length(250);                                  
        }
    }
}
