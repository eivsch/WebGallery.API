using DomainModel.Common.Enumerators;
using DomainModel.Generators.Interfaces;

namespace DomainModel.Factories
{
    public interface IGalleryGeneratorFactory
    {
        IGalleryGenerator GetGalleryGenerator(TagFilterMode mode);
    }
}
