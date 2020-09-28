﻿using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using DomainModel.Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class OnlyTaggedGifsGenerator : GalleryGenerator
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPictureRepository _pictureRepository;

        public OnlyTaggedGifsGenerator(ITagRepository tagRepository, IPictureRepository pictureRepository)
        {
            _tagRepository = tagRepository;
            _pictureRepository = pictureRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.OnlyTagged && galleryDescriptor.TagFilter.Mode != TagFilterMode.CustomInclusive)
                throw new NotSupportedException($"The '{nameof(OnlyTaggedGifsGenerator)}' does not support the current tag mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.GifMode == GifMode.Exclude)
                throw new NotSupportedException($"The '{nameof(OnlyTaggedGifsGenerator)}' does not support the gif mode '{GifMode.Exclude}'.");

            var list = new List<GeneratedItem>();
            var taggedImages = await _tagRepository.GetRandom(galleryDescriptor.TagFilter.Tags, 200);

            foreach (var taggedImage in taggedImages)
            {
                var picture = await _pictureRepository.FindById(taggedImage.PictureId);

                if (picture.Name.ToLower().EndsWith(".gif"))
                {
                    list.Add(new GeneratedItem
                    {
                        Id = picture.Id,
                        Index = picture.GlobalSortOrder,
                        Name = picture.Name,
                        Tags = taggedImage.TagName
                    });
                }
            }

            return list;
        }
    }
}
