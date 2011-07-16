using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    /*     	 
	 [FileContent] varbinary(max) FILESTREAM  NULL     
     */

    public sealed class DataFileMap : ClassMap<DataFile>
    {
        public DataFileMap()
        {
            Table("Files");

            Id(x => x.Id, "FileID")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Name, "FileTitle")
                .Not.Nullable()
                .Length(250);

            Map(x => x.TypeInfo, "FileTypeInfo")
                .CustomType<int>()
                .Not.Nullable();

            Map(x => x.Description, "FileDescription")
               .Nullable()
               .Length(250);

            Map(x => x.GUID, "FileGUID")
                .Default("NEWSEQUENTIALID()")
                .Unique()
                .Generated.Insert()
                .ReadOnly()
                .Not.Nullable();

            Map(x => x.Time, "FileCreateTime")
                .Default("Sysutcdatetime()")
                .CustomType("DateTime2")
                .Generated.Insert()
                .ReadOnly()
                .Not.Nullable();

            Map(x => x.Size, "FileSize")
                .Default("0")
                .Not.Update()
                .Not.Nullable();

            References(x => x.Packet, "FGroup_ID")
                .ForeignKey("FK_Files_FGroup")
                .Cascade.SaveUpdate()
                .Not.Nullable();

            HasMany<BackupFile>(x => x.Backups)
                .ForeignKeyCascadeOnDelete()
                .KeyColumn("File_ID")
                .Inverse()
                .Cascade.All();
        }
    }
}
