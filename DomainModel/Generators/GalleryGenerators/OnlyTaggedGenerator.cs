using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using System.Threading.Tasks;

namespace DomainModel.Generators.GalleryGenerators
{
    class OnlyTaggedGenerator : GalleryGenerator
    {
        private readonly ITagRepository _tagRepository;

        public OnlyTaggedGenerator(ITagRepository tagRepository, IPictureRepository pictureRepository)
            : base(pictureRepository)
        {
            _tagRepository = tagRepository;
        }

        protected override async Task AddGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            var taggedImages = await _tagRepository.GetRandom(null, galleryDescriptor.NumberOfItems);
            await AddGalleryItemsFromTaggedImages(gallery, taggedImages);
        }
    }
}
