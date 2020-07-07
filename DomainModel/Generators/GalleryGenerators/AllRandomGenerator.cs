using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class AllRandomGenerator : GalleryGenerator
    {
        private readonly IGalleryRepository _galleryRepository;

        public AllRandomGenerator(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.Undefined)
                throw new NotSupportedException($"The '{nameof(AllRandomGenerator)}' does not support the current tag mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.GifMode == GifMode.OnlyGifs)
                throw new NotSupportedException($"The '{nameof(AllRandomGenerator)}' does not support the current gif mode '{GifMode.OnlyGifs.Name}'.");

            List<GeneratedItem> list = new List<GeneratedItem>();
            var batch = await _galleryRepository.GetRandom(galleryDescriptor.NumberOfItems);

            foreach (var item in batch.GalleryItems)
            {
                if (item.MediaType == MediaType.Gif && galleryDescriptor.GifMode == GifMode.Exclude)
                    continue;

                list.Add(new GeneratedItem
                {
                    Id = item.Id,
                    Index = item.Index,
                    Name = item.Name
                });
            }

            return list;
        }
    }
}
