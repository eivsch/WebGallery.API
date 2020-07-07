using Application.Common.Interfaces;
using Application.Galleries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IGalleryService : IApplicationService<GalleryResponse>
    {
        Task<GalleryResponse> Get(GalleryRequest galleryRequest);
        Task<IEnumerable<GalleryResponse>> GetAll();
        Task<GalleryResponse> GetRandom(int itemsInGallery);
        Task<GalleryResponse> GetCustomizedRandom(int itemsInGallery, string tags, string tagFilteringMode);
        Task<GalleryResponse> Save(GalleryRequest request);
    }
}
