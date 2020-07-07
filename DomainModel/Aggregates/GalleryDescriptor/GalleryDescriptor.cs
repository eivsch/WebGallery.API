using DomainModel.Common.Enumerators;
using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Aggregates.GalleryDescriptor
{
    public class GalleryDescriptor : IAggregateRoot
    {
        private int _numberOfItems;
        private TagFilter _tagFilter;

        public virtual int NumberOfItems => _numberOfItems;
        public virtual TagFilter TagFilter => _tagFilter;

        private GalleryDescriptor()
        {

        }

        public static GalleryDescriptor Create(int numberOfItems)
        {
            return new GalleryDescriptor { _numberOfItems = numberOfItems };
        }

        public virtual void AddTagFilter(string tagsToFilter, string modeOfFiltering)
        {
            TagFilterMode filterMode;
            if (string.IsNullOrWhiteSpace(modeOfFiltering))
                filterMode = TagFilterMode.Undefined;
            else
                filterMode = TagFilterMode.Get(modeOfFiltering);

            if (filterMode == TagFilterMode.CustomInclusive || filterMode == TagFilterMode.CustomExclusive)
            {
                if (string.IsNullOrWhiteSpace(tagsToFilter))
                    throw new ArgumentException($"Tag list cannot be null when filter mode is set to '{filterMode}'");
            }

            TagFilter tagFilter = TagFilter.Create(filterMode);

            foreach (var tag in tagsToFilter.Split(','))
                tagFilter.AddTag(tag);

            _tagFilter = tagFilter;
        }
    }
}
