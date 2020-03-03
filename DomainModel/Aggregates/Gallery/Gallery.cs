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
        private int _numberOfItems;
        public virtual int NumberOfItems => _numberOfItems;

        private readonly List<string> _includeCategories;
        public virtual IReadOnlyCollection<string> IncludeCategories => _includeCategories.AsReadOnly();

        private readonly List<string> _excludeCategories;
        public virtual IReadOnlyCollection<string> ExcludeCategories => _excludeCategories.AsReadOnly();

        private readonly List<GalleryItem> _galleryItems;
        public virtual IReadOnlyCollection<GalleryItem> GalleryItems => _galleryItems.AsReadOnly();
    }
}
