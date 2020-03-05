using DomainModel.Common;
using DomainModel.Common.Enumerators;
using DomainModel.Common.Interfaces;
using DomainModel.Exceptions;
using System;
using System.Collections.Generic;

namespace DomainModel.Aggregates.Picture
{
    /// <summary>
    /// An aggregate representing a picture
    /// </summary>
    public class Picture : Entity, IAggregateRoot
    {
        private string _fileSystemPath;
        public virtual string FileSystemPath => _fileSystemPath;

        private string _name;
        public virtual string Name => _name;

        private string _directory;
        public virtual string Directory => _directory;

        private readonly List<string> _categories;
        public virtual IReadOnlyCollection<string> Categories => _categories;

        private MediaType _mediaType;
        public virtual MediaType MediaType => _mediaType;

        private Picture(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid().ToString();
            else
                Id = id;
        }

        public static Picture Create(string id, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new DomainLayerException($"Parameter {nameof(path)} cannot be empty");

            return new Picture(id) 
            { 
                _fileSystemPath = path 
            };
        }
    }
}
