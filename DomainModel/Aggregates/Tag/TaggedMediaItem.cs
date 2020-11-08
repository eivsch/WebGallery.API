using DomainModel.Common;
using System;

namespace DomainModel.Aggregates.Tag
{
    public class TaggedMediaItem : Entity
    {
        private TaggedMediaItem(string id) 
        {
            Id = id;
        }

        internal static TaggedMediaItem Create(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                throw new ArgumentNullException(nameof(itemId));

            return new TaggedMediaItem(itemId);
        }
    }
}
