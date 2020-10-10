using DomainModel.Aggregates.Picture;
using DomainModel.Aggregates.Picture.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<string> FindByGalleryIndex(string galleryId, int index)
        {
            switch (galleryId)
            {
                case "abc":
                    return @"C:\Eivsch\temp\pics\2017-NSX-3-1-1024x576.jpg";
                case "dfg":
                    return @"C:\Eivsch\temp\pics\8f35ba26fe296e36b3a96ee5416259b4.jpg";
                default:
                    return null;
            }
        }

        public Task<Picture> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Picture> FindById(string id)
        {
            return Picture.Create(
                id: id,
                name: "name",
                appPath: "app\\path",
                originalPath: "orig\\path",
                folderName: "folderName",
                folderId: "folderAppPath",
                folderSortOrder: 24,
                globalSortOrder: 2465,
                size: 34221,
                created: DateTime.Now);
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

        async Task<Picture> IPictureRepository.FindByIndex(int i)
        {
            return Picture.Create("app\\path", "orig\\path", "name", "folderName", "folderAppPath", 365, 39943, i, DateTime.Now);
        }

        async Task<Picture> IPictureRepository.FindByGalleryIndex(string galleryId, int index)
        {
            return Picture.Create(
                id: "123123",
                name: "name",
                appPath: "app\\path",
                originalPath: "orig\\path",
                folderName: "folderName",
                folderId: galleryId,
                folderSortOrder: 24,
                globalSortOrder: index,
                size: 34221,
                created: DateTime.Now);
        }

        public async Task<Picture> FindByAppPath(string appPath)
        {
            return Picture.Create(
                id: "123123",
                name: "name",
                appPath: appPath,
                originalPath: "orig\\path",
                folderName: "folderName",
                folderId: "hash1234",
                folderSortOrder: 24,
                globalSortOrder: 123,
                size: 34221,
                created: DateTime.Now);
        }
    }
}
