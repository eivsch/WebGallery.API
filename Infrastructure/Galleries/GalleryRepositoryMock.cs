using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task<Gallery> FindById(string id)
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
                Gallery.Create("abc", 1),
                Gallery.Create("dfg", 5),
            };

            return list;
        }

        public Task<Gallery> GetItems(Gallery gallery)
        {
            throw new NotImplementedException();
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
