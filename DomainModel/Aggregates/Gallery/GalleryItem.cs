using DomainModel.Common;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;

namespace DomainModel.Aggregates.Gallery
{
    public class GalleryItem : Entity
    {
        private int _indexGlobal;
        private int _indexGallery;
        private string _fileSystemPath;
        private string _name;
        private string _appPath;
        private MediaType _mediaType;
        private readonly List<string>  _tags;

        public virtual int IndexGlobal => _indexGlobal;
        public virtual int IndexGallery => _indexGallery;
        public virtual string FileSystemPath => _fileSystemPath;
        public virtual string Name => _name;
        public virtual string AppPath => _appPath;
        public virtual MediaType MediaType => _mediaType;
        public virtual IReadOnlyCollection<string> Tags => _tags;

        private GalleryItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid().ToString();
            else
                Id = id;

            _tags = new List<string>();
        }

        internal static GalleryItem Create(string id, int indexGlobal, int indexGallery, string name, string appPath, MediaType mediaType)
        {
            var item = new GalleryItem(id)
            {
                _indexGlobal = indexGlobal,
                _indexGallery = indexGallery,
                _name = name,
                _mediaType = mediaType,
                _appPath = appPath
            };

            return item;
        }

        internal virtual void AddTag(string tag)
        {
            if (!_tags.Contains(tag))
                _tags.Add(tag.ToUpper());
        }
    }
}
