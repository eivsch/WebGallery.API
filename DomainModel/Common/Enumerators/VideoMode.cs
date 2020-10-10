using DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Common.Enumerators
{
    public class VideoMode : Enumeration
    {
        public static VideoMode Include = new VideoMode(1, "Include");
        public static VideoMode Exclude = new VideoMode(2, "Exclude");
        public static VideoMode OnlyVideo = new VideoMode(3, "OnlyVideo");
        
        protected VideoMode()
        {
        }

        public VideoMode(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<VideoMode> List() => new[] { Include, Exclude, OnlyVideo };

        public static VideoMode Get(string name)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null) throw new DomainLayerException($"Possible values for VideoMode: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
