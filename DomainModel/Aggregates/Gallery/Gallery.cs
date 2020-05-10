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
        public virtual int NumberOfItems => _galleryItems.Count();

        private readonly List<string> _categories = new List<string>();
        public virtual IReadOnlyCollection<string> Categories => _categories.AsReadOnly();

        private readonly List<MediaType> _mediaTypes = new List<MediaType>();
        public virtual IReadOnlyCollection<MediaType> MediaTypes => _mediaTypes.AsReadOnly();

        private readonly List<GalleryItem> _galleryItems = new List<GalleryItem>();
        public virtual IReadOnlyCollection<GalleryItem> GalleryItems => _galleryItems.AsReadOnly();

        private Gallery(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid().ToString();
            else
                Id = id;
        }

        public static Gallery Create(string id)
        {
            var gallery = new Gallery(id);

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

        public virtual void AddGalleryItem(string galleryItemId, int index, string tags = "")
        {
            var galleryItem = GalleryItem.Create(galleryItemId, index);

            if (!string.IsNullOrWhiteSpace(tags))
            {
                foreach(var tag in tags.Split(','))
                {
                    galleryItem.AddTag(tag);
                }
            }
            
            _galleryItems.Add(galleryItem);
        }
    }
}
