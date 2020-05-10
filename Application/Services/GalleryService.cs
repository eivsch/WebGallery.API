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
                list.Add(Map(gal));
            }

            return list;
        }

        public async Task<GalleryResponse> Save(GalleryRequest request)
        {
            var aggregate = Gallery.Create(
                id: request.Id, 
                numberOfItems: request.GalleryPictures?.Count() ?? -1, 
                folderId: request.FolderId
            );

            foreach(var item in request.GalleryPictures)
            {
                aggregate.AddGalleryItem(galleryItemId: item.Id, index: item.Index);
            }

            aggregate = await _galleryRepository.Save(aggregate);

            return Map(aggregate);
        }

        private GalleryResponse Map(Gallery aggregate)
        {
            return new GalleryResponse
            {
                Id = aggregate.Id,
                ImageCount = aggregate.NumberOfItems,
                GalleryPictures = aggregate.GalleryItems.ToList().Select(i => Map(i)),
            };
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
