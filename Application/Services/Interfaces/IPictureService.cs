using Application.Common.Interfaces;
using Application.Pictures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IPictureService : IApplicationService<PictureResponse>
    {
        Task<PictureResponse> Get(string pictureId);
        Task<PictureResponse> Get(int index);
        Task<PictureResponse> GetRandomFromAlbum(string albumId);
        Task<PictureResponse> Add(PictureRequest pictureRequest);
        Task<IEnumerable<PictureResponse>> Search(string query);
    }
}
