﻿using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
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
            var aggregate = Gallery.Create($"random-{Guid.NewGuid()}".Substring(0, 15).ToLower(), 1);

            aggregate.AddGalleryItem("1", 1, "pic1", "tag1, tag2, tag3");
            aggregate.AddGalleryItem("2", 2, "untaggedPic");
            aggregate.AddGalleryItem("3", 3, "favoritePic", "favorite");
            aggregate.AddGalleryItem("4", 4, "pic4", "tag1, tag2, tag3");
            aggregate.AddGalleryItem("5", 5, "pic5", "tag1, tag2, tag3");
            aggregate.AddGalleryItem("6", 6, "pic6", "tag1, tag2, tag3");
            aggregate.AddGalleryItem("7", 7, "taggedVid.mp4", "tag1");
            aggregate.AddGalleryItem("8", 8, "untaggedVid.mp4");
            aggregate.AddGalleryItem("9", 9, "taggedGif.gif", "tag1");
            aggregate.AddGalleryItem("10", 10, "untaggedGif.gif");
            aggregate.AddGalleryItem("11", 11, "pic11", "tag1, tag2, tag3");
            aggregate.AddGalleryItem("12", 12, "pic12", "tag1, tag2, tag3");

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
