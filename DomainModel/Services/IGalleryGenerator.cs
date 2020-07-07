using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.GalleryDescriptor;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface IGalleryGenerator
    {
        Task<Gallery> GenerateGallery(GalleryDescriptor galleryDescriptor);
    }
}
