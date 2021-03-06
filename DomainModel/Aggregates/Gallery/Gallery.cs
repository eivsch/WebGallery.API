﻿using DomainModel.Common;
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
        private int _galleryItemIndexStart;
        private string _galleryName;
        private List<string> _tags = new List<string>();
        private readonly List<MediaType> _mediaTypes = new List<MediaType>();
        private readonly List<GalleryItem> _galleryItems = new List<GalleryItem>();

        private int _galleryItemsRunningIndex;

        public virtual int NumberOfItems => _numberOfItems;
        public virtual int GalleryItemIndexStart => _galleryItemIndexStart;
        public virtual string GalleryName => _galleryName;
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

        public static Gallery Create(string id, int numberOfItems, int galleryItemIndexStart = 1, string galleryName = "")
        {
            var gallery = new Gallery(id)
            {
                _numberOfItems = numberOfItems,
                _galleryItemIndexStart = galleryItemIndexStart,
                _galleryItemsRunningIndex = galleryItemIndexStart,
                _galleryName = galleryName
            };

            return gallery;
        }

        public virtual void AddGalleryItem(string galleryItemId, int indexGlobal, string name, string appPath, string tags = "")
        {
            if (string.IsNullOrWhiteSpace(appPath))
                throw new ArgumentNullException(nameof(appPath));

            GalleryItem galleryItem = _galleryItems.FirstOrDefault(i => i.Id == galleryItemId);
            if (galleryItem == null)
            {
                galleryItem = GalleryItem.Create(
                    id: galleryItemId, 
                    indexGlobal: indexGlobal, 
                    indexGallery: _galleryItemsRunningIndex++,
                    name: name, 
                    appPath: appPath,
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

        public void SetGalleryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
                
            _galleryName = name;
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
