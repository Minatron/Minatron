using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{



    public sealed class ImageMap : ClassMap<Image>
    {
        public ImageMap()
        {
            Table("Images");

            Id(x => x.Id, "ImageID")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Time, "ImageCreateTime")
               .Default("Sysutcdatetime()")
               .CustomType("DateTime2")
               .Not.Nullable();

            Map(x => x.Cloudiness, "ImageCloudiness")
              .Default("0")
              .Not.Nullable();

            Map(x => x.Polygon, "ImagePolygon")
                .CustomType(typeof(SqlGeographyUserType))
                .CustomSqlType("geography")
                .Nullable();

            Map(x => x.SurveyTime, "ImageSurveyTime")
               .CustomType("DateTime2")
               .Nullable();

            Map(x => x.SatelliteCatalogNumber, "Sat_Number")
                .Nullable();

            References(x => x.Level, "ILevel_ID")
                .ForeignKey("FK_Image_ILevel")
                .Cascade.SaveUpdate()
                .Not.Nullable();

            Map(x => x.PacketID, "DATA_ID")
                .Nullable();

            References(x => x.Sensor, "Sen_ID")
               .ForeignKey("FK_Image_Sen")
              
               .Cascade.SaveUpdate()
               .Nullable();

            /*
            References(x => x.Satellite, "Sat_ID")
               .ForeignKey("FK_Image_Sat")
               .Cascade.SaveUpdate()
               .Nullable();
            */
            References(x => x.ReceiveSession, "RSession_ID")
                .ForeignKey("FK_Image_RSession")
                .Cascade.SaveUpdate()
                .Nullable();


            HasMany(x => x.Attributes)
                //.Not.LazyLoad()
                .ForeignKeyCascadeOnDelete()
                .KeyColumn("Image_ID")
                .Inverse()
                .Cascade.All();

        }
    }

}
