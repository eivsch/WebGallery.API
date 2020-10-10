﻿using Application.Galleries;
using DomainModel.Aggregates.Gallery;
using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Aggregates.Gallery.Interfaces;
using System.Linq;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Factories;
using AutoMapper;

namespace Application.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly IGalleryGeneratorFactory _galleryGeneratorFactory;
        private readonly IMapper _mapper;

        public GalleryService(IGalleryRepository galleryRepository, IGalleryGeneratorFactory galleryGeneratorFactory, IMapper mapper)
        {
            _galleryRepository = galleryRepository ?? throw new ArgumentNullException(nameof(galleryRepository));
            _galleryGeneratorFactory = galleryGeneratorFactory;
            _mapper = mapper;
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
                var galleryRespone = _mapper.Map<GalleryResponse>(gal);
                list.Add(galleryRespone);
            }

            return list;
        }

        public async Task<GalleryResponse> GetCustomizedRandom(int itemsInGallery, string tags, string tagFilteringMode, string mediaFilterMode)
        {
            GalleryDescriptor descriptor = GalleryDescriptor.Create(itemsInGallery, mediaFilterMode);
            descriptor.SetTagFilter(tags, tagFilteringMode);

            var galleryGenerator = _galleryGeneratorFactory.GetGalleryGenerator(descriptor);

            var aggregate = await galleryGenerator.GenerateGallery(descriptor);

            return _mapper.Map<GalleryResponse>(aggregate);
        }

        public async Task<GalleryResponse> Save(GalleryRequest request)
        {
            var aggregate = Gallery.Create(
                id: request.Id, 
                numberOfItems: request.GalleryPictures?.Count() ?? -1
            );

            foreach(var item in request.GalleryPictures)
            {
                aggregate.AddGalleryItem(galleryItemId: item.Id, index: item.Index, name: "Unknown");
            }

            aggregate = await _galleryRepository.Save(aggregate);

            return _mapper.Map<GalleryResponse>(aggregate);
        }
    }
}
