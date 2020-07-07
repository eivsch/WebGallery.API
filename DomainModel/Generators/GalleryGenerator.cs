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
        public async Task<Gallery> GenerateGallery(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter == null)
                throw new ArgumentException("Descriptor object is not valid - TagFilter is null");

            var gallery = Gallery.Create(Guid.NewGuid().ToString(), galleryDescriptor.NumberOfItems);

            while (gallery.GalleryItems.Count < galleryDescriptor.NumberOfItems)
            {
                var items = await GenerateGalleryItems(galleryDescriptor);
                foreach (var item in items)
                {
                    if (gallery.GalleryItems.Count == galleryDescriptor.NumberOfItems)
                        break;

                    gallery.AddGalleryItem(item.Id, item.Index, tags: item.Tag);
                }
            }

            return gallery;
        }
        
        protected abstract Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor);
    }
}
