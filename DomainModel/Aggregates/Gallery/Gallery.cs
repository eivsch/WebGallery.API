using DomainModel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Aggregates.Gallery
{
    /// <summary>
    /// An aggregate representing a gallery of pictures
    /// </summary>
    public class Gallery : IAggregateRoot
    {
        private readonly List<GalleryItem> _galleryPictures;
        public virtual IReadOnlyCollection<GalleryItem> Pictures => _galleryPictures.OrderBy(p => p.Id).ToList().AsReadOnly();
    }
}
