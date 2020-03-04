using Application.Galleries;
using DomainModel.Aggregates.Gallery;
using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Aggregates.Gallery.Interfaces;

namespace Application.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository ?? throw new ArgumentNullException(nameof(galleryRepository));
        }

        public async Task<GalleryResponse> Generate(int itemCount)
        {
            Gallery gallery = Gallery.Create(itemCount);

            gallery = await _galleryRepository.GetItems(gallery);

            GalleryResponse galleryResponse = new GalleryResponse { GalleryItems = new List<Galleries.GalleryItem>() };
            foreach(var item in gallery.GalleryItems)
            {
                var itemResponse = new Galleries.GalleryItem
                {
                    Id = item.Id,
                    Path = item.FileSystemPath,
                    Categories = item.Categories
                };

                galleryResponse.GalleryItems.Add(itemResponse);
            }

            return galleryResponse;
        }

        public Task<GalleryResponse> Get(GalleryRequest galleryRequest)
        {
            throw new NotImplementedException();
        }
    }
}
