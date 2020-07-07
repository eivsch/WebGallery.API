using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using DomainModel.Common.Enumerators;
using DomainModel.Generators.GalleryGenerators;
using DomainModel.Generators.Interfaces;

namespace DomainModel.Factories
{
    public class GalleryGeneratorFactory : IGalleryGeneratorFactory
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPictureRepository _pictureRepository;

        public GalleryGeneratorFactory(ITagRepository tagRepository, IPictureRepository pictureRepository, IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
            _tagRepository = tagRepository;
            _pictureRepository = pictureRepository;
        }

        public IGalleryGenerator GetGalleryGenerator(TagFilterMode mode)
        {
            if (mode == TagFilterMode.CustomExclusive)
            {
                return new CustomExclusiveGenerator(_galleryRepository, _tagRepository);
            }
            else if (mode == TagFilterMode.CustomInclusive)
            {
                return new CustomInclusiveGenerator(_tagRepository, _pictureRepository);
            }
            else if (mode == TagFilterMode.OnlyTagged)
            {
                return new OnlyTaggedGenerator(_tagRepository, _pictureRepository);
            }
            else if (mode == TagFilterMode.OnlyUntagged)
            {
                return new OnlyUntaggedGenerator(_galleryRepository, _tagRepository);
            }
             
            return new AllRandomGenerator(_galleryRepository);
        }
    }
}
