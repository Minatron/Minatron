using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class BackupFileMap : ClassMap<BackupFile>
    {
        public BackupFileMap()
        {
            Table("RemovableFiles");

            Id(x => x.Id, "RFileID")                
                .Not.Nullable()                
                .GeneratedBy.Identity();

            Map(x => x.Time, "RFileStoreTime") 
                .Default("Sysutcdatetime()")
                .CustomType("DateTime2")                
                .Not.Update()
                .Not.Nullable();

            References(x => x.File, "File_ID")     
                .ForeignKey("FK_RemovableFiles_File")
               .Cascade.SaveUpdate()
               .Not.Nullable();

            References(x => x.Storage, "RStorage_ID")
                .ForeignKey("FK_RemovableFiles_RStorage")
               .Cascade.SaveUpdate()
               .Not.Nullable();
        }
    }
}
