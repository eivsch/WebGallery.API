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

        public Task<Picture> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> FindByIndex(int i)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Picture>> FindAll(string galleryId, int offset = 0)
        {
            return new List<Picture>
            {
                Picture.Create(
                    id: "dfgdsfgg",
                    name: "2017-NSX-3-1-1024x576.jpg",
                    globalSortOrder: 1,
                    folderSortOrder: 1
                ),
                Picture.Create(
                    id: "lkjjhg",
                    name: "b.jpg",
                    globalSortOrder: 2,
                    folderSortOrder: 2
                ),
            };
        }

        public void Remove(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Picture> Save(Picture aggregate)
        {
            throw new NotImplementedException();
        }

        Task<Picture> IPictureRepository.FindByIndex(int i)
        {
            throw new NotImplementedException();
        }

        Task<Picture> IPictureRepository.FindByGalleryIndex(string galleryId, int index)
        {
            throw new NotImplementedException();
        }
    }
}
