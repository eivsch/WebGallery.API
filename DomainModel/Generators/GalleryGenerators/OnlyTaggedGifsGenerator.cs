using DomainModel.Aggregates.GalleryDescriptor;
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

        public OnlyTaggedGifsGenerator( GalleryDescriptor galleryDescriptor, ITagRepository tagRepository, IPictureRepository pictureRepository)
            : base(galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode != TagFilterMode.OnlyTagged && galleryDescriptor.TagFilter.Mode != TagFilterMode.CustomInclusive)
                throw new NotSupportedException($"The '{nameof(OnlyTaggedGifsGenerator)}' does not support the current tag mode: {galleryDescriptor.TagFilter.Mode}");
            if (galleryDescriptor.MediaFilterMode == MediaFilterMode.ExcludeGifs)
                throw new NotSupportedException($"The '{nameof(OnlyTaggedGifsGenerator)}' does not support the gif mode '{MediaFilterMode.ExcludeGifs}'.");

            _tagRepository = tagRepository;
            _pictureRepository = pictureRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems()
        {
            var list = new List<GeneratedItem>();
            var tags = await _tagRepository.GetRandom(_galleryDescriptor.TagFilter.Tags, 200);

            foreach (var tag in tags)
            {
                foreach (var taggedImage in tag.MediaItems)
                {
                    var picture = await _pictureRepository.FindById(taggedImage.Id);

                    if (picture.Name.ToLower().EndsWith(".gif"))
                    {
                        list.Add(new GeneratedItem
                        {
                            Id = picture.Id,
                            Index = picture.GlobalSortOrder,
                            Name = picture.Name,
                            AppPath = picture.AppPath,
                            Tags = tag.Name
                        });
                    }
                }
            }

            return list;
        }
    }
}
