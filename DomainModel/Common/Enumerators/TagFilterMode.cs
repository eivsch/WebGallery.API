using DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Common.Enumerators
{
    public class TagFilterMode : Enumeration
    {
        public static TagFilterMode OnlyTagged = new TagFilterMode(1, "OnlyTagged");
        public static TagFilterMode OnlyUntagged = new TagFilterMode(2, "OnlyUntagged");
        public static TagFilterMode CustomInclusive = new TagFilterMode(3, "CustomInclusive");
        public static TagFilterMode CustomExclusive = new TagFilterMode(4, "CustomExclusive");
        public static TagFilterMode Undefined = new TagFilterMode(5, "Undefined");

        protected TagFilterMode()
        {
        }

        public TagFilterMode(int id, string name) : base(id, name)
        { }

        public static IEnumerable<TagFilterMode> List() => new[] { OnlyTagged, OnlyUntagged, CustomInclusive, CustomExclusive, Undefined };

        public static TagFilterMode Get(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null) throw new DomainLayerException($"Possible values for TagSelectorMode: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
