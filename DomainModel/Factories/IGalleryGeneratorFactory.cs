using DomainModel.Aggregates.GalleryDescriptor;
using DomainModel.Generators.Interfaces;

namespace DomainModel.Factories
{
    public interface IGalleryGeneratorFactory
    {
        IGalleryGenerator GetGalleryGenerator(GalleryDescriptor galleryDescriptor);
    }
}
