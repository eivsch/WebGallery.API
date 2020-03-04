using DomainModel.Common;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;

namespace DomainModel.Aggregates.Gallery
{
    public class GalleryItem : Entity
    {
        private string _fileSystemPath;
        public virtual string FileSystemPath => _fileSystemPath;

        private string _name;
        public virtual string Name => _name;

        private MediaType _mediaType;
        public virtual MediaType MediaType => _mediaType;

        private readonly List<string>  _categories;
        public virtual IReadOnlyCollection<string> Categories => _categories;

        private GalleryItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid().ToString();
            else
                Id = id;

            _categories = new List<string>();
        }

        internal static GalleryItem Create(string id, string fileSystemPath, string mediaType)
        {
            var item = new GalleryItem(id)
            {
                _fileSystemPath = fileSystemPath,
                _mediaType = MediaType.Get(mediaType)
            };

            return item;
        }

        internal virtual void AddCategory(string category)
        {
            if (!_categories.Contains(category))
                _categories.Add(category.ToUpper());
        }
    }
}
