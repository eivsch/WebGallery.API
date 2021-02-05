using Application.Common.Interfaces;
using Application.Pictures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IPictureService : IApplicationService<PictureResponse>
    {
        Task<IEnumerable<PictureResponse>> GetPictures(string galleryId, int offset = 0);
        Task<PictureResponse> Get(string pictureId);
        Task<PictureResponse> Get(int index);
        Task<PictureResponse> Get(string galleryId, int index);
        Task<PictureResponse> GetRandomFromAlbum(string albumId);
        Task<PictureResponse> GetByAppPath(string appPath);
        Task<PictureResponse> Add(PictureRequest pictureRequest);
    }
}
