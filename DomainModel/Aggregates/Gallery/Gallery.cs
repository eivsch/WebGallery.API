using DomainModel.Common;
using DomainModel.Common.Enumerators;
using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Aggregates.Gallery
{
    /// <summary>
    /// An aggregate representing a gallery
    /// </summary>
    public class Gallery : Entity, IAggregateRoot
    {
        private int _numberOfItems;
        public virtual int NumberOfItems => _numberOfItems;

        private readonly List<string> _categories = new List<string>();
        public virtual IReadOnlyCollection<string> Categories => _categories.AsReadOnly();

        private readonly List<MediaType> _mediaTypes = new List<MediaType>();
        public virtual IReadOnlyCollection<MediaType> MediaTypes => _mediaTypes.AsReadOnly();

        private readonly List<GalleryItem> _galleryItems = new List<GalleryItem>();
        public virtual IReadOnlyCollection<GalleryItem> GalleryItems => _galleryItems.AsReadOnly();

        private Gallery(string id)
        {
            Id = id;
        }

        public static Gallery Create(string id, int numberOfitems)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException($"Parameter {nameof(id)} cannot be empty");

            var gallery = new Gallery(id)
            {
                _numberOfItems = numberOfitems,
            };

            return gallery;
        }

        public virtual void AddCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentNullException(nameof(category));
                
            _categories.Add(category);
        }

        public virtual void AddMediaType(string mediaTypeName)
        {
            if (string.IsNullOrWhiteSpace(mediaTypeName))
                throw new ArgumentNullException(nameof(mediaTypeName));

            MediaType mediaType;
            switch (mediaTypeName.ToLower())
            {
                case "image":
                    mediaType = MediaType.Image;
                    break;
                case "gif":
                    mediaType = MediaType.Gif;
                    break;
                case "video":
                    mediaType = MediaType.Video;
                    break;
                default:
                    throw new ArgumentException($"Unsupported media type '{mediaTypeName}'");
            }

            _mediaTypes.Add(mediaType);
        }

        public virtual void AddGalleryItem(string galleryItemId, string fileSystemPath, string mediaType, string categories = "")
        {
            var galleryItem = GalleryItem.Create(galleryItemId, fileSystemPath, mediaType);

            if (!string.IsNullOrWhiteSpace(categories))
            {
                foreach(var category in categories.Split(','))
                {
                    galleryItem.AddCategory(category);
                }
            }
            
            _galleryItems.Add(galleryItem);
        }
    }
}
