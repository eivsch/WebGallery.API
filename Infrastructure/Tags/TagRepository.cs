using Infrastructure.Tags.DTO;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tags
{
    public class TagRepository : ITagRepositoy
    {
        private readonly IElasticClient _client;

        public TagRepository(IElasticClient elasticClient)
        {
            _client = elasticClient;
        }

        public async Task<List<string>> GetAllUniqueTags()
        {
            try
            {
                var result = await _client.SearchAsync<PictureDTO>(s => s
                    .Aggregations(a => a
                        .Terms("my_agg", st => st
                            .Field(f => f.Tags.First().Name.Suffix("keyword"))
                            .Size(200)
                        )
                    )
                    .Index("picture")
                );

                var list = new List<string>();
                foreach (var bucket in result.Aggregations.Terms("my_agg").Buckets)
                {
                    list.Add(bucket.Key);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
