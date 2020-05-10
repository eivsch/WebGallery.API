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
            throw new NotImplementedException();
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
                        Gallery.Create(bucket.Key)
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

        public async Task<List<Gallery>> GetRandom(int galleriesToGenerate, int itemsInGallery)
        {
            var galleryList = new List<Gallery>();
            var counter = 0;
            while (counter < galleriesToGenerate)
            {
                var searchResponse = await _client.SearchAsync<GalleryPictureDTO>(s => s
                    .Query(q => q
                        .FunctionScore(f => f
                            .Functions(fx => fx
                                .RandomScore(rng => rng.Seed(DateTime.Now.Millisecond))
                            )
                        )
                    )
                    .Size(itemsInGallery)
                    .Index("picture")
                );

                var gallery = Gallery.Create("");
                foreach (var pic in searchResponse.Documents)
                {
                    gallery.AddGalleryItem(pic.Id, pic.GlobalSortOrder);
                }

                galleryList.Add(gallery);

                counter++;
            }

            return galleryList;
        }
    }
}
