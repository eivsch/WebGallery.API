using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
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

        public IGalleryGenerator GetGalleryGenerator(GalleryDescriptor galleryDescriptor)
        {
            var mediaFilterMode = galleryDescriptor.MediaFilterMode;
            var tagMode = galleryDescriptor.TagFilter.Mode;
            
            if (mediaFilterMode == MediaFilterMode.OnlyGifs)
            {
                if (tagMode == TagFilterMode.CustomInclusive || tagMode == TagFilterMode.OnlyTagged)
                    return new OnlyTaggedGifsGenerator(galleryDescriptor, _tagRepository, _pictureRepository);
                
                return new OnlyGifsGenerator(galleryDescriptor, _galleryRepository);
            }
            else if (mediaFilterMode == MediaFilterMode.OnlyVideos)
            {
                return new OnlyVideoGenerator(galleryDescriptor, _galleryRepository);
            }
            else
            {
                if (tagMode == TagFilterMode.CustomExclusive)
                {
                    return new CustomExclusiveGenerator(galleryDescriptor, _galleryRepository, _tagRepository);
                }
                else if (tagMode == TagFilterMode.CustomInclusive)
                {
                    return new CustomInclusiveGenerator(galleryDescriptor, _tagRepository, _pictureRepository);
                }
                else if (tagMode == TagFilterMode.OnlyTagged)
                {
                    return new OnlyTaggedGenerator(galleryDescriptor, _tagRepository, _pictureRepository);
                }
                else if (tagMode == TagFilterMode.OnlyUntagged)
                {
                    return new OnlyUntaggedGenerator(galleryDescriptor, _galleryRepository, _tagRepository);
                }
            }
            
            return new AllRandomGenerator(galleryDescriptor, _galleryRepository);
        }
    }
}
