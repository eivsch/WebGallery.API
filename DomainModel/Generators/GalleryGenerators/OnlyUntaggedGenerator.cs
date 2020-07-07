using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Tag.Interfaces;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class OnlyUntaggedGenerator : GalleryGenerator
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ITagRepository _tagRepository;

        public OnlyUntaggedGenerator(IGalleryRepository galleryRepository, ITagRepository tagRepository)
        {
            _galleryRepository = galleryRepository;
            _tagRepository = tagRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.OnlyUntagged)
                throw new NotSupportedException($"The '{nameof(OnlyUntaggedGenerator)}' does not support the current tag mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.GifMode == GifMode.OnlyGifs)
                throw new NotSupportedException($"The '{nameof(OnlyUntaggedGenerator)}' does not support the current gif mode '{GifMode.OnlyGifs.Name}'.");

            var list = new List<GeneratedItem>();
            var batch = await _galleryRepository.GetRandom(galleryDescriptor.NumberOfItems);
            
            foreach (var item in batch.GalleryItems)
            {
                if (item.MediaType == MediaType.Gif && galleryDescriptor.GifMode == GifMode.Exclude)
                    continue;

                var tags = await _tagRepository.FindAllTagsForPicture(item.Id);
                if (tags is null || tags.Count() == 0)
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
