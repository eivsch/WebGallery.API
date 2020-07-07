using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag;
using DomainModel.Generators.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators
{
    public abstract class GalleryGenerator : IGalleryGenerator
    {
        protected readonly IPictureRepository _pictureRepository;

        protected Gallery gallery;

        public GalleryGenerator(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<Gallery> GenerateGallery(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter == null)
                throw new ArgumentException("Descriptor object is not valid - TagFilter is null");

            gallery = Gallery.Create(Guid.NewGuid().ToString(), galleryDescriptor.NumberOfItems);

            while (gallery.GalleryItems.Count < galleryDescriptor.NumberOfItems)
            {
                await AddGalleryItems(galleryDescriptor);
            }

            return gallery;
        }

        protected async Task AddGalleryItemsFromTaggedImages(Gallery gallery, IEnumerable<Tag> taggedImages)
        {
            foreach (var image in taggedImages)
            {
                if (gallery.GalleryItems.Count == gallery.NumberOfItems)
                    break;

                var picture = await _pictureRepository.FindById(image.PictureId);
                gallery.AddGalleryItem(picture.Id, picture.GlobalSortOrder);
            }
        }

        protected abstract Task AddGalleryItems(GalleryDescriptor galleryDescriptor);
    }
}
