using DomainModel.Aggregates.Metadata;
using DomainModel.Aggregates.Metadata.Interfaces;
using Elasticsearch.Net;
using Infrastructure.Services.DTO;
using Infrastructure.Services.ServiceModels;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MetadataService : IMetadataService
    {
        private readonly IElasticClient _client;

        public MetadataService(IElasticClient elasticClient)
        {
            _client = elasticClient;
        }

        public Task<MediaMetadata> GetPictureMetadata()
        {
            throw new NotImplementedException();
        }

        public Task<MediaMetadata> GetGifMetadata()
        {
            throw new NotImplementedException();
        }

        public Task<MediaMetadata> GetVideoMetadata()
        {
            throw new NotImplementedException();
        }

        public Task<TagMetadata> GetTagMetadata()
        {
            throw new NotImplementedException();
        }

        public Task<AlbumMetadata> GetAlbumMetadata()
        {
            throw new NotImplementedException();
        }

        private async Task<ItemDTO> GetMostRecentMediaItem(string type)
        {
            string searchTerm = GetMediaSearchTerm(type);

            var searchResponse = await _client.SearchAsync<ItemDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name.Suffix("keyword"))
                        .Query(searchTerm)
                    )
                )
                .Sort(s => s
                    .Descending(p => p.GlobalSortOrder)
                )
                .Size(1)
                .Index("picture")
            );

            var dto = searchResponse.Documents.Single();

            return new ItemDTO
            {
                Id = dto.Id,
                Name = dto.Name,
                GlobalSortOrder = dto.GlobalSortOrder,
                CreateTimestamp = dto.CreateTimestamp
            };
        }

        private async Task<long> Count(string type)
        {
            switch (type.ToLower())
            {
                case "picture":
                case "gif":
                case "video":
                    var searchTerm = GetMediaSearchTerm(type);

                    return await CountMediaItems(searchTerm);
                case "tags":
                    var countTagResult = await _client.CountAsync<ItemDTO>(c => c.Index("tag"));

                    return countTagResult.Count;
                case "album":
                    return await CountAlbums();
                default:
                    throw new ArgumentException();
            }

            async Task<long> CountMediaItems(string searchTerm)
            {
                var countPictureResult = await _client.CountAsync<ItemDTO>(c => c
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Name.Suffix("keyword"))
                            .Query(searchTerm)
                        )
                    )
                    .Index("picture")
                );

                return countPictureResult.Count;
            }

            async Task<long> CountAlbums()
            {
                var searchResponse = await _client.SearchAsync<ItemDTO>(s => s
                    .Aggregations(a => a
                        .Terms("my_agg", st => st
                            .Field(f => f.FolderId.Suffix("keyword"))
                            .Size(800)
                        )
                    )
                    .Index("picture")
                );

                return searchResponse.Aggregations.Terms("my_agg").Buckets.Count;
            }
        }

        private string GetMediaSearchTerm(string type)
        {
            string searchTerm = type switch
            {
                "picture" => "*.jpg",
                "gif" => "*.gif",
                "video" => "*.mp4",
                _ => throw new NotImplementedException(),
            };

            return searchTerm;
        }
    }
}
