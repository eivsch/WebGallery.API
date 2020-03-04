using Application.Common.Interfaces;
using Application.Galleries;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IGalleryService : IApplicationService<GalleryResponse>
    {
        Task<GalleryResponse> Generate(int itemCount);
        Task<GalleryResponse> Get(GalleryRequest galleryRequest);
    }
}
