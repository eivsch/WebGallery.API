﻿using DomainModel.Aggregates.Tag;
using DomainModel.Aggregates.Tag.Interfaces;
using Infrastructure.Tags.DTO;
using Microsoft.AspNetCore.Http;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly IElasticClient _client;
        private string _indexName;

        public TagRepository(IElasticClient elasticClient, IHttpContextAccessor httpContextAccessor)
        {
            _client = elasticClient;

            ResolveIndexName();

            void ResolveIndexName()
            {
                var httpRequestHeaders = httpContextAccessor.HttpContext.Request.Headers;
                var userId = httpRequestHeaders["Gallery-User"];
                if (!string.IsNullOrWhiteSpace(userId))
                    _indexName = $"{userId}_tag";
                else
                    _indexName = "tag";
            }
        }

        public async Task<Tag> Save(Tag aggregate)
        {
            foreach (var item in aggregate.MediaItems)
            {
                var dto = new TagDTO
                {
                    TagName = aggregate.Name,
                    PictureId = item.Id,
                    PictureAppPath = item.AppPath,
                    Added = item.Created
                };

                var indexRequest = new IndexRequest<TagDTO>(dto, _indexName);
                var response = await _client.IndexAsync(indexRequest);

                if (!response.IsValid)
                {
                    throw new Exception(response.DebugInformation);
                }
            }

            return aggregate;
        }

        public async Task<IEnumerable<Tag>> FindAllTagsForPicture(string pictureId)
        {
            var searchResponse = await SearchTagsByPictureId(pictureId);

            var tags = searchResponse.Documents;

            return BuildAggregatesFromDtoCollection(tags);
        }

        private async Task<ISearchResponse<TagDTO>> SearchTagsByPictureId(string pictureId)
        {
            var searchResponse = await _client.SearchAsync<TagDTO>(s => s
                .Query(q => q
                    .Terms(c => c
                        .Name("idsearch")
                        .Field(f => f.PictureId.Suffix("keyword"))
                        .Terms(pictureId)
                    )
                )
                .Size(1000)
                .Index(_indexName)
            );

            return searchResponse;
        }

        public async Task<List<Tag>> GetAllUniqueTags()
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
                    .Index(_indexName)
                );

                var list = new List<Tag>();
                foreach (var bucket in result.Aggregations.Terms("my_agg").Buckets)
                {
                    var aggregate = Tag.Create(bucket.Key);
                    aggregate.SetItemCount(Convert.ToInt32(bucket.DocCount));

                    list.Add(aggregate);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Tag>> GetRandom(IEnumerable<string> tags, int items)
        {
            var searchResponse = await _client.SearchAsync<TagDTO>(s => s
                .Query(q1 => q1
                    .FunctionScore(f => f
                        .Query(q2 => q2
                            .Terms(c => c
                                .Name("tagsearch")
                                .Field(f => f.TagName.Suffix("keyword"))
                                .Terms(tags)
                            )
                        )
                        .Functions(fx => fx
                            .RandomScore(rng => rng.Seed(DateTime.Now.Millisecond))
                        )
                    )
                )
                .Size(items)
                .Index(_indexName)
            );

            var response = searchResponse.Documents;

            return BuildAggregatesFromDtoCollection(response);
        }

        public async Task<Tag> Find(Tag aggregate)
        {
            var searchResponse = await _client.SearchAsync<TagDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.TagName)
                        .Query(aggregate.Name)
                    )
                )
                .Size(1000)
                .Index(_indexName)
            );

            var dtos = searchResponse.Documents;

            return BuildAggregatesFromDtoCollection(dtos).Single();
        }


        public async Task<int> DeleteTag(string pictureId, string tagName)
        {
            var searchResponse = await SearchTagsByPictureId(pictureId);

            // Map the internal _id field to the DTO 'Id' field
            var tags = searchResponse.Hits.Select(h =>
            {
                h.Source.Id = h.Id;
                return h.Source;
            }).ToList();

            int deleted = 0;
            foreach (var tag in tags)
            {
                if (tag.TagName.ToLower() == tagName.ToLower())
                {
                    await _client.DeleteAsync(new DeleteRequest(_indexName, tag.Id));
                    deleted++;
                }
            }

            return deleted;
        }

        private List<Tag> BuildAggregatesFromDtoCollection(IEnumerable<TagDTO> dtoList)
        {
            var allTags = new List<Tag>();
            foreach (var dto in dtoList)
            {
                Tag aggregate = allTags.FirstOrDefault(t => t.Name == dto.TagName);
                if (aggregate is null)
                {
                    aggregate = Tag.Create(dto.TagName);
                    allTags.Add(aggregate);
                }

                aggregate.AddMediaItem(dto.PictureId, dto.PictureAppPath, dto.Added);
            }

            return allTags;
        }

        #region Not Implemented

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

        public Task Remove(Tag aggregate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
