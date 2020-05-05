﻿using Application.Common.Interfaces;
using Application.Pictures;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IPictureService : IApplicationService<PictureResponse>
    {
        Task<PictureResponse> Get(string id);
        Task<string> Get(int id);
        Task<string> Get(string galleryId, int pictureId);
        Task<PictureResponse> Add(PictureRequest pictureRequest);
    }
}
