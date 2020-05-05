using DomainModel.Aggregates.Gallery;
using DomainModel.Aggregates.Gallery.Interfaces;
using Infrastructure.Galleries.DTO;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Galleries
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly IElasticClient _client;

        public GalleryRepository(IElasticClient elasticClient)
        {
            _client = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

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

        public async Task<List<Gallery>> GetAll()
        {
            try
            {
                var result = await _client.SearchAsync<GalleryDTO>(s => s
                    .Aggregations(a => a
                        .Terms("my_agg", st => st
                            .Field(f => f.FolderId.Suffix("keyword"))   // "keyword" is an ElasticSearch data-type: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/multi-fields.html
                        )
                    )
                    .Index("picture")
                );

                var list = new List<Gallery>();
                foreach(var bucket in result.Aggregations.Terms("my_agg").Buckets)
                {
                    list.Add(
                        Gallery.Create(bucket.Key, Convert.ToInt32(bucket.DocCount))
                    );
                }

                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
