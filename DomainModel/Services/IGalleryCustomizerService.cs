using DomainModel.Aggregates.Gallery;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface IGalleryCustomizerService
    {
        Task<Gallery> GetCustomizedGallery(Gallery gallery);
    }
}
