using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class CustomInclusiveGenerator : GalleryGenerator
    {
        private readonly ITagRepository _tagRepository;

        public CustomInclusiveGenerator(ITagRepository tagRepository, IPictureRepository pictureRepository)
            : base(pictureRepository)
        {
            _tagRepository = tagRepository;
        }

        protected override async Task AddGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            var taggedImages = await _tagRepository.GetRandom(galleryDescriptor.TagFilter.Tags, galleryDescriptor.NumberOfItems);
            await AddGalleryItemsFromTaggedImages(gallery, taggedImages);
        }
    }
}
