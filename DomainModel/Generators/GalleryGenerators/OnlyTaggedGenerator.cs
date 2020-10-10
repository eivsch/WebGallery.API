using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class OnlyTaggedGenerator : GalleryGenerator
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPictureRepository _pictureRepository;

        public OnlyTaggedGenerator(ITagRepository tagRepository, IPictureRepository pictureRepository)
        {
            _tagRepository = tagRepository;
            _pictureRepository = pictureRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.OnlyTagged)
                throw new NotSupportedException($"The '{nameof(OnlyTaggedGenerator)}' does not support the current tag mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.MediaFilterMode == MediaFilterMode.OnlyGifs)
                throw new NotSupportedException($"The '{nameof(OnlyTaggedGenerator)}' does not support the current gif mode '{MediaFilterMode.OnlyGifs.Name}'.");

            var list = new List<GeneratedItem>();
            var taggedImages = await _tagRepository.GetRandom(null, galleryDescriptor.NumberOfItems);

            foreach (var taggedImage in taggedImages)
            {
                var picture = await _pictureRepository.FindById(taggedImage.PictureId);
                
                if (picture.Name.ToLower().EndsWith(".gif") && galleryDescriptor.MediaFilterMode == MediaFilterMode.Exclude)
                    continue;

                list.Add(new GeneratedItem
                {
                    Id = picture.Id,
                    Index = picture.GlobalSortOrder,
                    Name = picture.Name,
                    Tags = taggedImage.TagName
                });
            }

            return list;
        }
    }
}
