using Application.Pictures;
using Application.Services.Interfaces;
using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    globalSortOrder: pictureRequest.GlobalSortOrder,
                    created: pictureRequest.Created
                );

            foreach (var tag in pictureRequest.Tags)
                pic.AddTag(tag);

            pic = await _pictureRepository.Save(pic);

            return Map(pic);
        }

        public async Task<PictureResponse> Get(string pictureId)
        {
            var pic = await _pictureRepository.FindById(pictureId);

            return Map(pic);
        }

        public async Task<PictureResponse> Get(int index)
        {
            var pic = await _pictureRepository.FindByIndex(index);

            return Map(pic);
        }

        public async Task<PictureResponse> Get(string galleryId, int pictureId)
        {
            var pic = await _pictureRepository.FindByGalleryIndex(galleryId, pictureId);

            return Map(pic);
        }

        public async Task<IEnumerable<PictureResponse>> GetPictures(string galleryId, int offset = 0)
        {
            var pics = await _pictureRepository.FindAll(galleryId, offset);

            var list = new List<PictureResponse>();
            pics.ToList().ForEach(p => list.Add(Map(p)));

            return list;
        }

        private PictureResponse Map(Picture pic)
        {
            return new PictureResponse
            {
                Id = pic.Id,
                Name = pic.Name,
                AppPath = pic.AppPath,
                OriginalPath = pic.OriginalPath,
                FolderName = pic.FolderName,
                FolderId = pic.FolderId,
                FolderSortOrder = pic.FolderSortOrder,
                GlobalSortOrder = pic.GlobalSortOrder,
                Size = pic.Size,
                CreateTimestamp = pic.CreateTimestamp,
                Tags = pic.Tags
            };
        }
    }
}
