using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Aggregates.Tag
{
    public class Tag : IAggregateRoot
    {
        private string _tagName;
        private int? _itemCount;
        private List<TaggedMediaItem> _mediaItems = new List<TaggedMediaItem>();

        public virtual string TagName => _tagName;
        public virtual int ItemCount => _itemCount ?? _mediaItems.Count;
        public virtual IReadOnlyCollection<TaggedMediaItem> MediaItems => _mediaItems.AsReadOnly();

        private Tag() { }

        public static Tag Create(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));

            return new Tag()
            {
                _tagName = tagName,
            };
        }

        public virtual void AddMediaItem(string itemId)
        {
            TaggedMediaItem taggedItem = _mediaItems.FirstOrDefault(i => i.Id == itemId);
            if (taggedItem is null)
            {
                taggedItem = TaggedMediaItem.Create(itemId);

                _mediaItems.Add(taggedItem);
            }
        }

        public virtual void SetItemCount(int itemCount) => _itemCount = itemCount;
    }
}
