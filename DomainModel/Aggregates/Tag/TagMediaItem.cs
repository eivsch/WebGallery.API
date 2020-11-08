using DomainModel.Common;
using System;

namespace DomainModel.Aggregates.Tag
{
    public class TagMediaItem : Entity
    {
        private DateTime _created;

        public virtual DateTime Created => _created;

        private TagMediaItem(string id) 
        {
            Id = id;
        }

        internal static TagMediaItem Create(string itemId, DateTime created)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                throw new ArgumentNullException(nameof(itemId));

            return new TagMediaItem(itemId)
            {
                _created = created
            };
        }
    }
}
