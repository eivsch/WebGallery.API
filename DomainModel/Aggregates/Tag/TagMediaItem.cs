using DomainModel.Common;
using System;

namespace DomainModel.Aggregates.Tag
{
    public class TagMediaItem : Entity
    {
        private DateTime _created;
        private string _appPath;

        public virtual DateTime Created => _created;
        public virtual string AppPath => _appPath;

        private TagMediaItem(string id) 
        {
            Id = id;
        }

        internal static TagMediaItem Create(string itemId, string itemAppPath, DateTime created)
        {
            if (string.IsNullOrWhiteSpace(itemId))
                throw new ArgumentNullException(nameof(itemId));

            return new TagMediaItem(itemId)
            {
                _created = created,
                _appPath = itemAppPath
            };
        }
    }
}
