using Application.Galleries;
using DomainModel.Aggregates.Gallery;
using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Aggregates.Gallery.Interfaces;
using System.Linq;
using DomainModel.Services;
using DomainModel.Aggregates.GalleryDescriptor;

namespace Application.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IGalleryGeneratorFactory _galleryGeneratorFactory;

        public GalleryService(IGalleryRepository galleryRepository, IGalleryGeneratorFactory galleryGeneratorFactory)
        {
            _galleryRepository = galleryRepository ?? throw new ArgumentNullException(nameof(galleryRepository));
            _galleryGeneratorFactory = galleryGeneratorFactory;
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

        public async Task<GalleryResponse> GetRandom(int itemsInGallery)
        {
            var randomGallery = await _galleryRepository.GetRandom(itemsInGallery);

            return Map(randomGallery);
        }

        public async Task<GalleryResponse> GetCustomizedRandom(int itemsInGallery, string tags, string tagFilteringMode)
        {
            GalleryDescriptor descriptor = GalleryDescriptor.Create(itemsInGallery);
            descriptor.AddTagFilter(tags, tagFilteringMode);

            var galleryGenerator = _galleryGeneratorFactory.GetGalleryGenerator(descriptor);

            var aggregate = await galleryGenerator.GenerateGallery(descriptor);

            return Map(aggregate);
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
