using Application.Pictures;
using Application.Services.Interfaces;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository ?? throw new ArgumentNullException(nameof(pictureRepository));
        }

        public async Task<PictureResponse> Add(PictureRequest pictureRequest)
        {
            Picture pic = Picture.Create
                (
                    appPath: pictureRequest.AppPath,
                    originalPath: pictureRequest.OriginalPath,
                    name: pictureRequest.Name,
                    folderName: pictureRequest.FolderName,
                    folderAppPath: pictureRequest.FolderAppPath,
                    folderSortOrder: pictureRequest.FolderSortOrder,
                    size: pictureRequest.Size,
                    globalSortOrder: pictureRequest.GlobalSortOrder
                );

            var picResp = await _pictureRepository.Save(pic);

            return new PictureResponse
            {
                Id = picResp.Id,
                Path = picResp.AppPath
            };
        }

        public async Task<PictureResponse> Get(string id)
        {
            var pic = await _pictureRepository.FindById(id);

            return new PictureResponse
            {
                Id = pic.Id,
                Path = pic.OriginalPath
            };
        }

        public async Task<string> Get(int id)
        {
            var pic = await _pictureRepository.FindByIndex(id);

            return pic;
        }
    }
}
