using Infrastructure.Services.DTO;
using Infrastructure.Services.ServiceModels;
using Nest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MetadataService : IMetadataService
    {
        private readonly IElasticClient _client;
        private (string Picture, string Tags, string Gif, string Video, string Album) Types => ("picture", "tags", "gif", "video", "album");

        public MetadataService(IElasticClient elasticClient)
        {
            _client = elasticClient;
        }

        public async Task<int> GetGlobalSortOrderMax()
        {
            var mostRecent = await GetMostRecentMediaItemByGlobalindex();

            return mostRecent?.GlobalSortOrder ?? 0;
        }

        public async Task<MediaMetadata> GetPictureMetadata()
        {
            var count = await Count(Types.Picture);
            //var mostLiked = GetMostLikedItem(Types.Picture);
            var mostRecent = await GetMostRecentMediaItemByGlobalindex(Types.Picture);

            return new MediaMetadata
            {
                Count = (int) count,
                MostLikedCount = 0,
                MostLikedName = "N/A",
                MostRecentName = mostRecent?.Name ?? "N/A",
                MostRecentTimestamp = mostRecent?.CreateTimestamp ?? DateTime.MinValue
            };
        }

        public async Task<MediaMetadata> GetGifMetadata()
        {
            var count = await Count(Types.Gif);
            //var mostLiked = GetMostLikedItem(Types.Gif);
            var mostRecent = await GetMostRecentMediaItemByGlobalindex(Types.Gif);

            return new MediaMetadata
            {
                Count = (int) count,
                MostLikedCount = 0,
                MostLikedName = "N/A",
                MostRecentName = mostRecent?.Name ?? "N/A",
                MostRecentTimestamp = mostRecent?.CreateTimestamp ?? DateTime.MinValue
            };
        }

        public async Task<MediaMetadata> GetVideoMetadata()
        {
            var count = await Count(Types.Video);
            //var mostLiked = GetMostLikedItem(Types.Video);
            var mostRecent = await GetMostRecentMediaItemByGlobalindex(Types.Video);

            return new MediaMetadata
            {
                Count = (int) count,
                MostLikedCount = 0,
                MostLikedName = "N/A",
                MostRecentName = mostRecent?.Name ?? "N/A",
                MostRecentTimestamp = mostRecent?.CreateTimestamp ?? DateTime.MinValue
            };
        }

        public async Task<TagMetadata> GetTagMetadata()
        {
            var count = await Count(Types.Tags);
            var aggregatedTagInfo = await GetAggregatedTagInfo();
            var mostRecent = await GetMostRecentTag();

            return new TagMetadata
            {
                Count = (int) count,
                UniqueCount = aggregatedTagInfo.TotalUnique,
                MostPopularCount = (int) aggregatedTagInfo.MostPopularCount,
                MostPopularName = aggregatedTagInfo.MostPopularName,
                MostRecentMediaName = mostRecent.MediaName,
                MostRecentTagName = mostRecent.TagName
            };

            #region TAG METHODS

            async Task<(string MostPopularName, long MostPopularCount, int TotalUnique)> GetAggregatedTagInfo()
            {
                var searchResponse = await _client.SearchAsync<TagDTO>(s => s
                    .Aggregations(a => a
                        .Terms("my_agg", st => st
                            .Field(f => f.TagName.Suffix("keyword"))
                            .Size(800)
                        )
                    )
                    .Index("tag")
                );

                var buckets = searchResponse.Aggregations.Terms("my_agg").Buckets;
                var bucket = buckets.OrderByDescending(b => b.DocCount).FirstOrDefault();

                return (bucket?.Key ?? "N/A", bucket?.DocCount ?? 0, buckets.Count);
            }

            async Task<(string TagName, string MediaName)> GetMostRecentTag()
            {
                var tagSearchResponse = await _client.SearchAsync<TagDTO>(s => s
                    .Sort(s => s
                        .Descending(p => p.Added)
                    )
                    .Size(1)
                    .Index("tag")
                );

                var tagDto = tagSearchResponse.Documents.FirstOrDefault();
                if (tagDto != null)
                {
                    var mediaItemSearchResponse = await _client.SearchAsync<ItemDTO>(s => s
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Id)
                            .Query(tagDto.PictureId)
                        )
                    )
                    .Size(1)
                    .Index("picture")
                    );

                    var itemDto = mediaItemSearchResponse.Documents.Single();

                    return (tagDto.TagName, itemDto.Name);
                }

                return ("N/A", "N/A");
            }

            #endregion
        }

        public async Task<AlbumMetadata> GetAlbumMetadata()
        {
            var count = await Count(Types.Album);
            //var mostLiked = GetMostLikedAlbum();
            var mostRecentItem = await GetMostRecentMediaItemByGlobalindex();

            return new AlbumMetadata
            {
                Count = (int) count,
                MostLikedCount = 0,
                MostLikedName = "N/A",
                MostRecentName = mostRecentItem?.FolderName ?? "N/A",
                MostRecentTimestamp = mostRecentItem?.CreateTimestamp ?? DateTime.MinValue
            };
        }

        private async Task<ItemDTO> GetMostRecentMediaItemByGlobalindex(string type = "")
        {
            string searchTerm = string.IsNullOrWhiteSpace(type) ? "" : GetMediaSearchTerm(type);

            var searchResponse = await _client.SearchAsync<ItemDTO>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query(searchTerm)
                    )
                )
                .Sort(s => s
                    .Descending(p => p.GlobalSortOrder)
                )
                .Size(1)
                .Index("picture")
            );

            var dto = searchResponse.Documents.FirstOrDefault();

            return dto;
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
                    var countTagResult = await _client.CountAsync<TagDTO>(c => c.Index("tag"));

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
                            .Field(f => f.Name)
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
