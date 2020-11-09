using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Aggregates.Tag
{
    public class Tag : IAggregateRoot
    {
        private string _name;
        private int? _itemCount;
        private List<TagMediaItem> _mediaItems = new List<TagMediaItem>();

        public virtual string Name => _name;
        public virtual int ItemCount => _itemCount ?? _mediaItems.Count;
        public virtual IReadOnlyCollection<TagMediaItem> MediaItems => _mediaItems.AsReadOnly();

        private Tag() { }

        public static Tag Create(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));

            return new Tag()
            {
                _name = tagName,
            };
        }

        public virtual void AddMediaItem(string itemId, DateTime? created)
        {
            if (!created.HasValue)
                created = DateTime.UtcNow;

            TagMediaItem taggedItem = _mediaItems.FirstOrDefault(i => i.Id == itemId);
            if (taggedItem is null)
            {
                taggedItem = TagMediaItem.Create(itemId, created.Value);

                _mediaItems.Add(taggedItem);
            }
        }

        public virtual void SetItemCount(int itemCount) => _itemCount = itemCount;
    }
}
