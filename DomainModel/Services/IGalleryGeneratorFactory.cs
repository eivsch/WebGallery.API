using DomainModel.Aggregates.GalleryDescriptor;

namespace DomainModel.Services
{
    public interface IGalleryGeneratorFactory
    {
        IGalleryGenerator GetGalleryGenerator(GalleryDescriptor galleryDescriptor);
    }
}
