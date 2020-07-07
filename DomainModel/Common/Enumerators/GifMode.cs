using DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Common.Enumerators
{
    public class GifMode : Enumeration
    {
        public static GifMode Include = new GifMode(1, "Include");
        public static GifMode Exclude = new GifMode(2, "Exclude");
        public static GifMode OnlyGifs = new GifMode(3, "OnlyGifs");

        protected GifMode()
        {
        }

        public GifMode(int id, string name) : base(id, name)
        { }

        public static IEnumerable<GifMode> List() => new[] { Include, Exclude, OnlyGifs };

        public static GifMode Get(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null) throw new DomainLayerException($"Possible values for GifFilterMode: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
