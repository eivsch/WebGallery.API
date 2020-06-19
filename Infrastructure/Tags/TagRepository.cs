using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using Infrastructure.Tags.DTO;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly IElasticClient _client;

        public TagRepository(IElasticClient elasticClient)
        {
            _client = elasticClient;
        }

        public Task<Tag> Find(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> FindAll(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> FindAllTagsForPicture(string pictureId)
        {
            var searchResponse = await _client.SearchAsync<TagDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.PictureId)
                        .Query(pictureId)
                    )
                )
                .Size(1000)
                .Index("tag")
            );

            var tags = searchResponse.Documents;

            return tags.Select(s => BuildAggregateFromDto(s));
        }

        public async Task<List<string>> GetAllUniqueTags()
        {
            try
            {
                var result = await _client.SearchAsync<TagDTO>(s => s
                    .Aggregations(a => a
                        .Terms("my_agg", st => st
                            .Field(f => f.TagName.Suffix("keyword"))
                            .Size(1000)
                        )
                    )
                    .Index("tag")
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

        public void Remove(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> Save(Tag aggregate)
        {
            var dto = new TagDTO
            {
                TagName = aggregate.TagName,
                PictureId = aggregate.PictureId,
                Added = DateTime.UtcNow
            };

            var indexRequest = new IndexRequest<TagDTO>(dto, "tag");
            var response = await _client.IndexAsync(indexRequest);
            
            if (!response.IsValid)
            {
                throw new Exception(response.DebugInformation);
            }

            return aggregate;
        }

        private Tag BuildAggregateFromDto(TagDTO dto)
        {
            return Tag.Create(tagName: dto.TagName, pictureId: dto.PictureId);
        }

        public async Task<IEnumerable<Tag>> FindAll(string tagName)
        {
            var searchResponse = await _client.SearchAsync<TagDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.TagName)
                        .Query(tagName)
                    )
                )
                .Size(1000)
                .Index("tag")
            );

            var tags = searchResponse.Documents;

            return tags.Select(s => BuildAggregateFromDto(s));
        }
    }
}
