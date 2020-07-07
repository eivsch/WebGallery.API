using DomainModel.Common;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Aggregates.GalleryDescriptor
{
    public class TagFilter : ValueObject
    {
        private TagFilterMode _mode;

        private List<string> _tags = new List<string>();

        public virtual TagFilterMode Mode => _mode;
        public virtual IReadOnlyCollection<string> Tags => _tags.AsReadOnly();

        private TagFilter()
        { }

        internal static TagFilter Create(TagFilterMode filteringMode)
        {
            return new TagFilter { _mode = filteringMode };
        }

        internal void AddTag(string tag)
        {
            tag = tag.ToUpper();

            if (!Tags.Contains(tag))
                _tags.Add(tag);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _mode;
            yield return _tags;
        }
    }
}
