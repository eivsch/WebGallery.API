using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class CustomInclusiveGenerator : GalleryGenerator
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPictureRepository _pictureRepository;

        public CustomInclusiveGenerator(ITagRepository tagRepository, IPictureRepository pictureRepository)
        {
            _tagRepository = tagRepository;
            _pictureRepository = pictureRepository;
        }

        protected override async Task<List<GeneratedItem>> GenerateGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            List<GeneratedItem> list = new List<GeneratedItem>();

            var taggedImages = await _tagRepository.GetRandom(galleryDescriptor.TagFilter.Tags, galleryDescriptor.NumberOfItems);

            foreach (var image in taggedImages)
            {
                var picture = await _pictureRepository.FindById(image.PictureId);
                list.Add(new GeneratedItem
                {
                    Id = picture.Id,
                    Index = picture.GlobalSortOrder
                });
            }

            return list;
        }
    }
}
