using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Galleries
{
    public class GalleryRepository : IGalleryRepository
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

        public Task<Gallery> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Gallery> GetItems(Gallery gallery)
        {
            gallery.AddGalleryItem("1", "test/path/pic1.jpg", "image", categories: "tractor,football");
            gallery.AddGalleryItem("2", "test/path/pic2.jpg", "image");
            gallery.AddGalleryItem("3", "test/path/pic3.jpg", "image");
            gallery.AddGalleryItem("4", "test/path/pic4.jpg", "image", categories: "football");
            gallery.AddGalleryItem("5", "test/path/pic5.jpg", "image");

            return gallery;
        }

        public void Remove(Gallery aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Gallery> Save(Gallery aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
