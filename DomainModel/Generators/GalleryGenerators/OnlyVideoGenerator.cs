using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class OnlyVideoGenerator : GalleryGenerator
    {
        private readonly IGalleryRepository _galleryRepository;

        public OnlyVideoGenerator(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.Undefined)
                throw new NotSupportedException($"The '{nameof(OnlyVideoGenerator)}' does not support the current tag filter mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.MediaFilterMode != MediaFilterMode.OnlyVideos)
                throw new NotSupportedException($"The '{nameof(OnlyVideoGenerator)}' does not support the current media filter mode '{galleryDescriptor.MediaFilterMode.Name}'.");

            var list = new List<GeneratedItem>();
            var batch = await _galleryRepository.GetRandom(200);

            foreach (var item in batch.GalleryItems)
            {
                if (item.MediaType == MediaType.Video)
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
    }
}
