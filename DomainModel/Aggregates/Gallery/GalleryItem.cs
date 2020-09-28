﻿using DomainModel.Common;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;

namespace DomainModel.Aggregates.Gallery
{
    public class GalleryItem : Entity
    {
        private int _index;
        private string _fileSystemPath;
        private string _name;
        private MediaType _mediaType;
        private readonly List<string>  _tags;

        public virtual int Index => _index;
        public virtual string FileSystemPath => _fileSystemPath;
        public virtual string Name => _name;
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

        internal static GalleryItem Create(string id, int index, string name, MediaType mediaType)
        {
            var item = new GalleryItem(id)
            {
                _index = index,
                _name = name,
                _mediaType = mediaType
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
