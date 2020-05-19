using DomainModel.Common;
using DomainModel.Common.Interfaces;
using System;

namespace DomainModel.Aggregates.Tag
{
    public class Tag : IAggregateRoot
    {
        private string _tagName;
        private string _pictureId;

        public virtual string TagName => _tagName;
        public virtual string PictureId => _pictureId;

        private Tag() { }

        //private Tag(string id = "") 
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //        id = Guid.NewGuid().ToString();

        //    Id = id;
        //}

        public static Tag Create(string tagName, string pictureId)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));
            if (string.IsNullOrWhiteSpace(pictureId))
                throw new ArgumentNullException(nameof(pictureId));

            return new Tag()
            {
                _tagName = tagName,
                _pictureId = pictureId
            };
        }
    }
}
