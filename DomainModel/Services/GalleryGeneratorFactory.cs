using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using DomainModel.Aggregates.Tag.Interfaces;
using DomainModel.Common.Enumerators;

namespace DomainModel.Services
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

        public IGalleryGenerator GetGalleryGenerator(GalleryDescriptor galleryDescriptor)
        {
            if (galleryDescriptor.TagFilter.Mode == TagFilterMode.CustomExclusive)
            {
                return new CustomExclusiveGenerator(_galleryRepository, _tagRepository, _pictureRepository);
            }
            else if (galleryDescriptor.TagFilter.Mode == TagFilterMode.CustomInclusive)
            {
                return new CustomInclusiveGenerator(_tagRepository, _pictureRepository);
            }
            else if (galleryDescriptor.TagFilter.Mode == TagFilterMode.OnlyTagged)
            {
                return new OnlyTaggedGenerator(_tagRepository, _pictureRepository);
            }
            else if (galleryDescriptor.TagFilter.Mode == TagFilterMode.OnlyUntagged)
            {
                return new OnlyUntaggedGenerator(_galleryRepository, _tagRepository, _pictureRepository);
            }
             
            return new AllRandomGenerator(_pictureRepository, _galleryRepository);
        }
    }
}
