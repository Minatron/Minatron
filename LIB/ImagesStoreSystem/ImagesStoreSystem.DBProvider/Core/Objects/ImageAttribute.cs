using System;

namespace ImagesStoreSystem.DBProvider.Core
{
    public class ImageAttribute : UpdatableObject
    {
        protected ImageAttribute()
        { }
        public ImageAttribute(Image image, AttributeTitle attribute)
            : this()
        {
            if (image == null) throw new ArgumentNullException("image");
            if (attribute == null) throw new ArgumentNullException("attribute");

            image.Attributes.Add(this);
            Image = image;
            Attribute = attribute;
        }

        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual Image Image { get; protected set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual AttributeTitle Attribute { get; protected set; }
        /// <summary>
        /// NOT NULL
        /// </summary>
        public virtual string Value { get; set; }
    }
}
