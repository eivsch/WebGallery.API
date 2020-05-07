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
        Task<string> Get(int pictureIndexGlobal);
        Task<string> Get(string galleryId, int pictureIndex);
        Task<PictureResponse> Add(PictureRequest pictureRequest);
    }
}
