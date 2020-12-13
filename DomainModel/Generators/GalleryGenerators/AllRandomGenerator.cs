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

        public AllRandomGenerator(GalleryDescriptor galleryDescriptor, IGalleryRepository galleryRepository)
            : base(galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.Undefined)
                throw new NotSupportedException($"The '{nameof(AllRandomGenerator)}' does not support the current tag mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.MediaFilterMode == MediaFilterMode.OnlyGifs || galleryDescriptor.MediaFilterMode == MediaFilterMode.OnlyVideos)
                throw new NotSupportedException($"The '{nameof(AllRandomGenerator)}' does not support the current media filter mode '{galleryDescriptor.MediaFilterMode}'.");

            _galleryRepository = galleryRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems()
        {
            List<GeneratedItem> list = new List<GeneratedItem>();
            var batch = await _galleryRepository.GetRandom(_galleryDescriptor.NumberOfItems);

            foreach (var item in batch.GalleryItems)
            {
                if (item.MediaType == MediaType.Gif && _galleryDescriptor.MediaFilterMode == MediaFilterMode.ExcludeGifs)
                    continue;

                list.Add(new GeneratedItem
                {
                    Id = item.Id,
                    Index = item.IndexGlobal,
                    Name = item.Name,
                    AppPath = item.AppPath,
                    Tags = string.Join(",", item.Tags)
                });
            }

            return list;
        }
    }
}
