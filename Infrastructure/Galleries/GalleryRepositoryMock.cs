using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
using Infrastructure.Helpers;
using Infrastructure.Pictures.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Galleries
{
    public class GalleryRepositoryMock : IGalleryRepository
    {
        public Task<Gallery> Find(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Gallery>> FindAll(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Gallery> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gallery>> GetAll()
        {
            var list = new List<Gallery>()
            {
                Gallery.Create("gallery1", 7),
                Gallery.Create("subGal1", 5),
                Gallery.Create("subGal2", 4),
                Gallery.Create("subGal3", 5),
            };

            return list;
        }

        public async Task<Gallery> FillEmptyGalleryWithItems(Gallery gallery)
        {
            return AddItemsHelper(gallery);
        }

        public async Task<Gallery> GetRandom(int itemsInGallery)
        {
            if (itemsInGallery > 21)
                itemsInGallery = 21;

            var aggregate = Gallery.Create($"random-{Guid.NewGuid()}".Substring(0, 15).ToLower(), 1);

            var allData = new MockData().GetAll();
            allData.ShuffleList();

            var galleryItems = allData.Take(itemsInGallery);
            foreach (var item in galleryItems)
            {
                aggregate.AddGalleryItem(item.Id, item.GlobalSortOrder, item.Name);
            }

            return aggregate;
        }

        public void Remove(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> Save(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        private Gallery AddItemsHelper(Gallery aggregate)
        {
            var galleryItems = new MockData().GetAll().Where(d => d.FolderId == aggregate.Id && d.FolderSortOrder >= aggregate.GalleryItemIndexStart);
            foreach (var item in galleryItems)
                aggregate.AddGalleryItem(item.Id, item.GlobalSortOrder, item.Name);

            return aggregate;
        }
    }
}
