using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class OnlyGifsGenerator : GalleryGenerator
    {
        private readonly IGalleryRepository _galleryRepository;

        public OnlyGifsGenerator(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.Undefined)
                throw new NotSupportedException($"The '{nameof(OnlyGifsGenerator)}' does not support the current tag mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.MediaFilterMode == MediaFilterMode.Exclude)
                throw new NotSupportedException($"The '{nameof(OnlyGifsGenerator)}' does not support the current gif mode '{MediaFilterMode.Exclude.Name}'.");

            var list = new List<GeneratedItem>();
            var batch = await _galleryRepository.GetRandom(200);

            foreach (var item in batch.GalleryItems)
            {
                if (item.MediaType == MediaType.Gif)
                {
                    list.Add(new GeneratedItem
                    {
                        Id = item.Id,
                        Index = item.Index,
                        Name = item.Name
                    });
                }
            }

            return list;
        }

        protected override Task<List<GeneratedItem>> GenerateGalleryItems()
        {
            throw new NotImplementedException();
        }
    }
}
