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

        private string _folderId;
        public virtual string FolderId => _folderId;
        
        private readonly List<string> _tags = new List<string>();
        public virtual IReadOnlyCollection<string> Tags => _tags.AsReadOnly();

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

        public static Gallery Create(string id, int numberOfItems, string folderId = "")
        {
            var gallery = new Gallery(id)
            {
                _numberOfItems = numberOfItems,
                _folderId = folderId
            };

            return gallery;
        }

        public virtual void AddTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));
                
            _tags.Add(tag);
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
