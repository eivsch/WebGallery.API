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
        private string _name;
        public virtual string Name => _name;

        private string _directory;
        public virtual string Directory => _directory;

        private int _numberOfItems;
        public virtual int NumberOfItems => _numberOfItems;

        private readonly List<string> _categories;
        public virtual IReadOnlyCollection<string> Categories => _categories.AsReadOnly();

        private readonly List<GalleryItem> _galleryItems;
        public virtual IReadOnlyCollection<GalleryItem> GalleryItems => _galleryItems.AsReadOnly();
    }
}
