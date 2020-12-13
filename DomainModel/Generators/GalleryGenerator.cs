using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Generators.GalleryGenerators;
using DomainModel.Generators.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators
{
    public abstract class GalleryGenerator : IGalleryGenerator
    {
        private const int MAX_ITERATIONS = 20;

        protected readonly GalleryDescriptor _galleryDescriptor;

        public GalleryGenerator(GalleryDescriptor galleryDescriptor)
        {
            _galleryDescriptor = galleryDescriptor;
        }

        public async Task<Gallery> GenerateGallery()
        {
            if (_galleryDescriptor.TagFilter == null)
                throw new ArgumentException("Descriptor object is not valid - TagFilter is null");

            var gallery = Gallery.Create(Guid.NewGuid().ToString(), _galleryDescriptor.NumberOfItems);

            int counter = 0;
            while (gallery.GalleryItems.Count < _galleryDescriptor.NumberOfItems && counter < MAX_ITERATIONS)
            {
                var items = await GenerateGalleryItems();
                foreach (var item in items)
                {
                    if (gallery.GalleryItems.Count == _galleryDescriptor.NumberOfItems)
                        break;

                    gallery.AddGalleryItem(
                        galleryItemId: item.Id,
                        indexGlobal: item.Index,
                        name: item.Name,
                        appPath: item.AppPath,
                        tags: item.Tags
                    );
                }

                counter++;
            }

            return gallery;
        }
        
        protected abstract Task<List<GeneratedItem>> GenerateGalleryItems();
    }
}
