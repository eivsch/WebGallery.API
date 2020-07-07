using DomainModel.Aggregates.Gallery.Interfaces;
using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Aggregates.Picture.Interfaces;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    class AllRandomGenerator : GalleryGeneratorBase
    {
        private readonly IGalleryRepository _galleryRepository;

        public AllRandomGenerator(IPictureRepository pictureRepository, IGalleryRepository galleryRepository)
            : base(pictureRepository)
        {
            _galleryRepository = galleryRepository;
        }

        protected override async Task AddGalleryItems(GalleryDescriptor galleryDescriptor)
        {
            gallery = await _galleryRepository.GetRandom(galleryDescriptor.NumberOfItems);
        }
    }
}
