﻿using DomainModel.Common;
using DomainModel.Common.Enumerators;
using DomainModel.Common.Interfaces;
using DomainModel.Exceptions;
using DomainModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Aggregates.Picture
{
    /// <summary>
    /// An aggregate representing a picture
    /// </summary>
    public class Picture : Entity, IAggregateRoot
    {
        private string _appPath;
        private string _originalPath;
        private string _name;
        private string _folderName;
        private string _folderId;
        private int _folderSortOrder;
        private int _globalSortOrder;
        private int _size;
        private DateTime _createTimestamp;
        private List<string> _tags = new List<string>();
        private List<string> _detectedObjects = new List<string>();

        public virtual string Name => _name;
        public virtual string AppPath => _appPath;
        public virtual string OriginalPath => _originalPath;
        public virtual string FolderName => _folderName;
        public virtual string FolderId => _folderId;
        public virtual int FolderSortOrder => _folderSortOrder;
        public virtual int GlobalSortOrder => _globalSortOrder;
        public virtual int Size => _size;
        public virtual DateTime CreateTimestamp => _createTimestamp;
        public virtual IReadOnlyCollection<string> Tags => _tags;
        public virtual IReadOnlyCollection<string> DetectedObjects => _detectedObjects.OrderBy(s => s).ToList().AsReadOnly();

        private Picture(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid().ToString();
            else
                Id = id;
        }

        public static Picture Create(
            string appPath, 
            string originalPath, 
            string name, 
            string folderName, 
            string folderAppPath, 
            int folderSortOrder, 
            int size, 
            int globalSortOrder, 
            DateTime? created = null
        )
        {
            if (string.IsNullOrWhiteSpace(appPath))
                throw new ArgumentNullException($"Parameter {nameof(appPath)} cannot be empty");

            string id = CryptographicHelper.HashValues(appPath);
            
            if (string.IsNullOrWhiteSpace(folderAppPath))
                folderAppPath = appPath.Replace(name, "").Replace("\\", "").Replace("/", "");
                
            string folderId = CryptographicHelper.HashValues(folderAppPath);

            return new Picture(id)
            {
                _name = name,
                _appPath = appPath,
                _originalPath = originalPath,
                _folderName = folderName,
                _folderId = folderId,
                _folderSortOrder = folderSortOrder,
                _size = size,
                _createTimestamp = created ?? DateTime.UtcNow,
                _globalSortOrder = globalSortOrder
            };
        }

        public static Picture Create(
            string id, 
            string name, 
            string appPath,
            string originalPath,
            string folderName,
            string folderId,
            int folderSortOrder, 
            int globalSortOrder, 
            int size,
            DateTime created
        )
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException($"Parameter {nameof(id)} cannot be empty");
            if(string.IsNullOrWhiteSpace(folderId))
                throw new ArgumentNullException($"Parameter {nameof(folderId)} cannot be empty");

            return new Picture(id)
            {
                _name = name,
                _appPath = appPath,
                _originalPath = originalPath,
                _folderName = folderName,
                _folderId = folderId,
                _folderSortOrder = folderSortOrder,
                _globalSortOrder = globalSortOrder,
                _size = size,
                _createTimestamp = created
            };
        }

        public void AddTag(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));

            if (!_tags.Contains(tagName))
                _tags.Add(tagName);
        }

        public void AddDetectedObject(string objectName)
        {
            if (string.IsNullOrWhiteSpace(objectName))
                throw new ArgumentNullException(nameof(objectName));

            _detectedObjects.Add(objectName);
        }
    }
}
