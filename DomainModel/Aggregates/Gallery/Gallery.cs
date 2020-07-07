using DomainModel.Common;
using DomainModel.Common.Enumerators;
using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Aggregates.Gallery
{
    /// <summary>
    /// An aggregate representing a gallery
    /// </summary>
    public class Gallery : Entity, IAggregateRoot
    {
        private int _numberOfItems;
        private string _folderId;
        private List<string> _tags = new List<string>();
        private readonly List<MediaType> _mediaTypes = new List<MediaType>();
        private readonly List<GalleryItem> _galleryItems = new List<GalleryItem>();

        public virtual int NumberOfItems => _numberOfItems;
        public virtual string FolderId => _folderId;
        public virtual IReadOnlyCollection<string> Tags => _tags.AsReadOnly();
        public virtual IReadOnlyCollection<MediaType> MediaTypes => _mediaTypes.AsReadOnly();
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

        public virtual void AddGalleryItem(string galleryItemId, int index, string name, string tags = "")
        {
            GalleryItem galleryItem = _galleryItems.FirstOrDefault(i => i.Id == galleryItemId);
            if (galleryItem == null)
            {
                galleryItem = GalleryItem.Create(
                    id: galleryItemId, 
                    index: index, 
                    name: name, 
                    mediaType: ParseMediaTypeFromName(name)
                );

                _galleryItems.Add(galleryItem);
            }

            if (!string.IsNullOrWhiteSpace(tags))
            {
                foreach(var tag in tags.Split(','))
                    galleryItem.AddTag(tag);
            }
        }

        private MediaType ParseMediaTypeFromName(string name)
        {
            if (name.ToLower().EndsWith(".gif"))
                return MediaType.Gif;
            else if (name.ToLower().EndsWith(".mp4"))
                return MediaType.Video;
            // Etc.

            return MediaType.Image;
        }
    }
}
