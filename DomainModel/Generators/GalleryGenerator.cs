﻿using DomainModel.Aggregates.Gallery;
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

        public async Task<Gallery> GenerateGallery(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter == null)
                throw new ArgumentException("Descriptor object is not valid - TagFilter is null");

            var gallery = Gallery.Create(Guid.NewGuid().ToString(), galleryDescriptor.NumberOfItems);

            int counter = 0;
            while (gallery.GalleryItems.Count < galleryDescriptor.NumberOfItems && counter < MAX_ITERATIONS)
            {
                var items = await GenerateGalleryItems(galleryDescriptor);
                foreach (var item in items)
                {
                    if (gallery.GalleryItems.Count == galleryDescriptor.NumberOfItems)
                        break;

                    gallery.AddGalleryItem(
                        galleryItemId: item.Id, 
                        index: item.Index, 
                        name: item.Name, 
                        tags: item.Tags
                    );
                }

                counter++;
            }

            return gallery;
        }
        
        protected abstract Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor);
    }
}
