using DomainModel.Common;
using System;

namespace DomainModel.Aggregates.Tag
{
    public class TagMediaItem : Entity
    {
        private TagMediaItem(string id) 
        {
            Id = id;
        }

        internal static TagMediaItem Create(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                throw new ArgumentNullException(nameof(itemId));

            return new TagMediaItem(itemId);
        }
    }
}
