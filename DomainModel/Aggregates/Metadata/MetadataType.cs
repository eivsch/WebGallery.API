using DomainModel.Common;
using DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Aggregates.Metadata
{
    public class MetadataType : Enumeration
    {
        public static MetadataType Gif = new MetadataType(1, "Gif");
        public static MetadataType Video = new MetadataType(4, "Video");
        public static MetadataType Picture = new MetadataType(4, "Picture");
        public static MetadataType Album = new MetadataType(4, "Album");
        public static MetadataType Tag = new MetadataType(4, "Tag");

        protected MetadataType()
        {

        }

        public MetadataType(int id, string name) : base(id, name)
        {

        }

        public static IEnumerable<MetadataType> List() => new[] { Gif, Video, Picture, Album, Tag, };

        public static MetadataType Get(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null) throw new DomainLayerException($"Possible values for {nameof(MetadataType)}: {String.Join(",", List().Select(s => s.Name))}");

            return state;

        }
    }
}
