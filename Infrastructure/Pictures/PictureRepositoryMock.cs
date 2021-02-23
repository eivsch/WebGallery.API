using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using Infrastructure.Pictures.DTO.ElasticSearch;
using Infrastructure.Pictures.Mock;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Pictures
{
    public class PictureRepositoryMock : IPictureRepository
    {
        public Task<Picture> Find(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Picture>> FindAll(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Picture> FindById(string id)
        {
            var dto = new MockData().GetAll().FirstOrDefault(d => d.Id == id);

            return CreateFromDto(dto);
        }

        public Task<Picture> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Picture>> FindAll(string galleryId, int offset = 0)
        {
            return new List<Picture>
            {
                Picture.Create(
                    id: "1",
                    name: "name",
                    appPath: "app\\path",
                    originalPath: "orig\\path",
                    folderName: "folderName",
                    folderId: galleryId,
                    folderSortOrder: 24,
                    globalSortOrder: 2465,
                    size: 34221,
                    created: DateTime.Now
                ),
                Picture.Create(
                    id: "2",
                    name: "name",
                    appPath: "app\\path",
                    originalPath: "orig\\path",
                    folderName: "folderName",
                    folderId: galleryId,
                    folderSortOrder: 24,
                    globalSortOrder: 2465,
                    size: 34221,
                    created: DateTime.Now
                ),
            };
        }

        public void Remove(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<Picture> Save(Picture aggregate)
        {
            return aggregate;
        }

        public async Task<Picture> FindByIndex(int i)
        {
            var dto = new MockData().GetAll().FirstOrDefault(d => d.GlobalSortOrder == i);
            return CreateFromDto(dto);
        }

        public async Task<Picture> FindByGalleryIndex(string galleryId, int index)
        {
            var pictureDto = new MockData().GetAll().FirstOrDefault(d => d.FolderId == galleryId && d.FolderSortOrder == index);

            return CreateFromDto(pictureDto);
        }

        private Picture CreateFromDto(PictureDTO pictureDto)
        {
            return Picture.Create(
                    id: pictureDto.Id,
                    name: pictureDto.Name,
                    appPath: pictureDto.AppPath,
                    originalPath: pictureDto.OriginalPath,
                    folderName: pictureDto.FolderName,
                    folderId: pictureDto.FolderId,
                    folderSortOrder: pictureDto.FolderSortOrder,
                    globalSortOrder: pictureDto.GlobalSortOrder,
                    size: pictureDto.Size,
                    created: pictureDto.CreateTimestamp);
        }

        public async Task<Picture> FindByAppPath(string appPath)
        {
            var dto = new MockData().GetAll().FirstOrDefault(d => d.AppPath == appPath);
            return CreateFromDto(dto);
        }

        public Task<Picture> FindRandomFromAlbum(string albumId)
        {
            throw new NotImplementedException();
        }
    }
}
