using FluentNHibernate.Mapping;
using ImagesStoreSystem.DBProvider.Core;

namespace ImagesStoreSystem.DBProvider.Mapping
{
    public sealed class ImageAttributeMap : ClassMap<ImageAttribute>
    {
        public ImageAttributeMap()
        {
            Table("ImageAttribute");

            Id(x => x.Id, "IAttID")                
                .Not.Nullable()                
                .GeneratedBy.Identity();

            References(x => x.Image, "Image_ID")
               .ForeignKey("FK_ImageAtt_Image")
               .Cascade.SaveUpdate() //on delete cascade
               .Not.Nullable();

            References(x => x.Attribute, "AttType_ID")
               .ForeignKey("FK_ImageAtt_Type")
               .Cascade.SaveUpdate() //on delete cascade
               .Not.Nullable();           

            Map(x => x.Value, "IAttValue")              
              .Length(250)
              .Not.Nullable();
        }
    }
   
}
