using DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Common.Enumerators
{
    public class MediaType : Enumeration
    {
        public static MediaType Image = new MediaType(1, "Image");
        public static MediaType Gif = new MediaType(2, "Gif");
        public static MediaType Video = new MediaType(3, "Video");

        protected MediaType()
        {
        }

        public MediaType(int id, string name) : base(id, name)
        { }

        public static IEnumerable<MediaType> List() => new[] { Image, Gif, Video };

        public static MediaType Get(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null) throw new DomainLayerException($"Possible values for GalleryItemType: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
