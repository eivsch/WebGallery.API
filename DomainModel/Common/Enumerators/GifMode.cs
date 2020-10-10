using DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Common.Enumerators
{
    public class MediaFilterMode : Enumeration
    {
        public static MediaFilterMode Include = new MediaFilterMode(1, "Include");
        public static MediaFilterMode Exclude = new MediaFilterMode(2, "Exclude");
        public static MediaFilterMode OnlyGifs = new MediaFilterMode(3, "OnlyGifs");
        public static MediaFilterMode OnlyVideos = new MediaFilterMode(4, "OnlyVideos");

        protected MediaFilterMode()
        {
        }

        public MediaFilterMode(int id, string name) : base(id, name)
        { }

        public static IEnumerable<MediaFilterMode> List() => new[] { Include, Exclude, OnlyGifs, OnlyVideos };

        public static MediaFilterMode Get(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null) throw new DomainLayerException($"Possible values for FilterMode: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
