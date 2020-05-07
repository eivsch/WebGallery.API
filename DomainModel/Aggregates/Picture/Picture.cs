using DomainModel.Common;
using DomainModel.Common.Enumerators;
using DomainModel.Common.Interfaces;
using DomainModel.Exceptions;
using DomainModel.Utils;
using System;
using System.Collections.Generic;

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

        public virtual string Name => _name;
        public virtual string AppPath => _appPath;
        public virtual string OriginalPath => _originalPath;
        public virtual string FolderName => _folderName;
        public virtual string FolderId => _folderId;
        public virtual int FolderSortOrder => _folderSortOrder;
        public virtual int GlobalSortOrder => _globalSortOrder;
        public virtual int Size => _size;
        public virtual DateTime? CreateTimestamp => _createTimestamp;


        private Picture(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                Id = Guid.NewGuid().ToString();
            else
                Id = id;
        }

        public static Picture Create(string appPath, string originalPath, string name, string folderName, string folderAppPath, int folderSortOrder, int size, int globalSortOrder)
        {
            if (string.IsNullOrWhiteSpace(appPath))
                throw new ArgumentNullException($"Parameter {nameof(appPath)} cannot be empty");
            if (globalSortOrder == 0)
                throw new ArgumentException("A valid global sort order must be provded.");

            string id = CryptographicHelper.HashValues(appPath);
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
                _createTimestamp = DateTime.UtcNow,
                _globalSortOrder = globalSortOrder
            };
        }

        public static Picture Create(string id, string name, int globalSortOrder, int folderSortOrder)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException($"Parameter {nameof(id)} cannot be empty");
            if (globalSortOrder == 0)
                throw new ArgumentException("A valid global sort order must be provded.");

            return new Picture(id)
            {
                _name = name,
                _globalSortOrder = globalSortOrder,
                _folderSortOrder = folderSortOrder
            };
        }
    }
}
