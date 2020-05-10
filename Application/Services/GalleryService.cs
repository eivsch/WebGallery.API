using Application.Galleries;
using DomainModel.Aggregates.Gallery;
using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Aggregates.Gallery.Interfaces;
using System.Linq;

namespace Application.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository ?? throw new ArgumentNullException(nameof(galleryRepository));
        }

        public Task<GalleryResponse> Get(GalleryRequest galleryRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GalleryResponse>> GetAll()
        {
            var allGalleries = await _galleryRepository.GetAll();

            List<GalleryResponse> list = new List<GalleryResponse>();
            foreach(var gal in allGalleries)
            {
                list.Add(new GalleryResponse
                {
                    Id = gal.Id,
                    ImageCount = gal.NumberOfItems
                });
            }

            return list;
        }

        public async Task<IEnumerable<GalleryResponse>> GetRandom(int numberOfGalleries, int itemsInGallery)
        {
            var randomGalleries = await _galleryRepository.GetRandom(numberOfGalleries, itemsInGallery);

            List<GalleryResponse> list = new List<GalleryResponse>();
            foreach (var gal in randomGalleries)
            {
                list.Add(new GalleryResponse
                {
                    Id = gal.Id,
                    ImageCount = gal.NumberOfItems,
                    GalleryPictures = gal.GalleryItems.ToList().Select(i => Map(i)),
                });
            }

            return list;
        }

        private GalleryPicture Map(GalleryItem item)
        {
            return new GalleryPicture
            {
                Id = item.Id,
                Index = item.Index
            };
        }
    }
}
