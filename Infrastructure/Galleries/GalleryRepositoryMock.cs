using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
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
            return Helper(gallery);
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

        private Gallery Helper(Gallery aggregate)
        {
            switch (aggregate.Id)
            {
                case "gallery1":
                    aggregate.AddGalleryItem("id_1", 1, "car.jpg");
                    aggregate.AddGalleryItem("id_2", 2, "car3.jpg");
                    aggregate.AddGalleryItem("id_3", 3, "dude.gif");
                    aggregate.AddGalleryItem("id_4", 4, "future-cars-lead.jpg");
                    aggregate.AddGalleryItem("id_5", 5, "Powerfly5EU_19_23179_A_Portrait.jpg");
                    aggregate.AddGalleryItem("id_6", 6, "shuttle.png");
                    aggregate.AddGalleryItem("id_7", 7, "small.mp4");
                    break;
                case "subGal1":
                    aggregate.AddGalleryItem("id_8", 8, "car2.jpg");
                    aggregate.AddGalleryItem("id_9", 9, "Rower-gorski-MTB-INDIANA-Fat-Bike-M26-Czarny-skos.jpg");
                    aggregate.AddGalleryItem("id_10", 10, "rower-MTB-street-jump-dirt-bike-rad-26-Mafiabikes-Blackjack-D-Green-new-neu-2020-3.jpg");
                    aggregate.AddGalleryItem("id_11", 11, "unnamed.gif");
                    aggregate.AddGalleryItem("id_12", 12, "woodland_wanderer_dribbble.gif");
                    break;
                case "subGal2":
                    aggregate.AddGalleryItem("id_13", 13, "sub2_1.jpg");
                    aggregate.AddGalleryItem("id_14", 14, "sub2_2.jpg");
                    aggregate.AddGalleryItem("id_15", 15, "sub2_3.jpg");
                    aggregate.AddGalleryItem("id_16", 16, "sub2_4.jpg");
                    break;
                case "subGal3":
                    aggregate.AddGalleryItem("id_17", 17, "sub3_1.jpg");
                    aggregate.AddGalleryItem("id_18", 18, "sub3_2.jpg");
                    aggregate.AddGalleryItem("id_19", 19, "sub3_3.jpg");
                    aggregate.AddGalleryItem("id_20", 20, "sub3_4.jpg");
                    aggregate.AddGalleryItem("id_21", 21, "sub3_5.jpg");
                    break;
            }

            var copy = Gallery.Create(aggregate.Id, aggregate.NumberOfItems, aggregate.ItemIndexStart);

            return aggregate;
        }
    }
}
