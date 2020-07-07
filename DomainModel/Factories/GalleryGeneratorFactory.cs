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
            var gifMode = galleryDescriptor.GifMode;
            var tagMode = galleryDescriptor.TagFilter.Mode;
            
            if (gifMode == GifMode.OnlyGifs)
            {
                if (tagMode == TagFilterMode.CustomInclusive || tagMode == TagFilterMode.OnlyTagged)
                    return new OnlyTaggedGifsGenerator(_tagRepository, _pictureRepository);
                
                return new OnlyGifsGenerator(_galleryRepository);
            }
            else
            {
                if (tagMode == TagFilterMode.CustomExclusive)
                {
                    return new CustomExclusiveGenerator(_galleryRepository, _tagRepository);
                }
                else if (tagMode == TagFilterMode.CustomInclusive)
                {
                    return new CustomInclusiveGenerator(_tagRepository, _pictureRepository);
                }
                else if (tagMode == TagFilterMode.OnlyTagged)
                {
                    return new OnlyTaggedGenerator(_tagRepository, _pictureRepository);
                }
                else if (tagMode == TagFilterMode.OnlyUntagged)
                {
                    return new OnlyUntaggedGenerator(_galleryRepository, _tagRepository);
                }
            }
            
            return new AllRandomGenerator(_galleryRepository);
        }
    }
}
