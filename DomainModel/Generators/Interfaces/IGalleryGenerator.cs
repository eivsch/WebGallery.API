using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.GalleryDescriptor;
using System.Threading.Tasks;

namespace DomainModel.Generators.Interfaces
{
    public interface IGalleryGenerator
    {
        Task<Gallery> GenerateGallery();
    }
}
