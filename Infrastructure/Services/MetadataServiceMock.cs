using Infrastructure.Pictures.Mock;
using Infrastructure.Services.ServiceModels;
using Infrastructure.Tags.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MetadataServiceMock : IMetadataService
    {
        private (string Picture, string Tags, string Gif, string Video, string Album) Types => ("picture", "tags", "gif", "video", "album");

        public async Task<MediaMetadata> GetGifMetadata()
        {
            var count = Count(Types.Gif);
            var mostLiked = GetMostLikedItem(Types.Gif);
            var mostRecent = GetMostRecentMediaItem(Types.Gif);

            return new MediaMetadata
            {
                Count = count,
                MostLikedCount = mostLiked.Count,
                MostLikedName = mostLiked.Name,
                MostRecentName = mostRecent.Name,
                MostRecentTimestamp = mostRecent.Timestamp
            };
        }

        public async Task<MediaMetadata> GetPictureMetadata()
        {
            var count = Count(Types.Picture);
            var mostLiked = GetMostLikedItem(Types.Picture);
            var mostRecent = GetMostRecentMediaItem(Types.Picture);

            return new MediaMetadata
            {
                Count = count,
                MostLikedCount = mostLiked.Count,
                MostLikedName = mostLiked.Name,
                MostRecentName = mostRecent.Name,
                MostRecentTimestamp = mostRecent.Timestamp
            };
        }

        public async Task<MediaMetadata> GetVideoMetadata()
        {
            var count = Count(Types.Video);
            var mostLiked = GetMostLikedItem(Types.Video);
            var mostRecent = GetMostRecentMediaItem(Types.Video);

            return new MediaMetadata
            {
                Count = count,
                MostLikedCount = mostLiked.Count,
                MostLikedName = mostLiked.Name,
                MostRecentName = mostRecent.Name,
                MostRecentTimestamp = mostRecent.Timestamp
            };
        }

        public async Task<TagMetadata> GetTagMetadata()
        {
            var count = Count(Types.Tags);
            var mostPopular = GetMostPopularTag();
            var mostRecent = GetMostRecent();
            var uniqueCount = GetUniqueCount();

            return new TagMetadata
            {
                Count = count,
                UniqueCount = uniqueCount,
                MostPopularCount = mostPopular.Count,
                MostPopularName = mostPopular.Name,
                MostRecentMediaName = mostRecent.MediaName,
                MostRecentTagName = mostRecent.TagName
            };

            (string Name, int Count) GetMostPopularTag()
            {
                var tagData = new MockDataTags().GetAll();
                var tagGroup = tagData
                    .GroupBy(g => new
                    {
                        g.TagName
                    })
                    .Select(grp => new
                    {
                        grp.Key.TagName,
                        Count = grp.Count(),
                    })
                    .OrderByDescending(grp => grp.Count)
                    .First();

                return (tagGroup.TagName, tagGroup.Count);
            }

            (string TagName, string MediaName) GetMostRecent()
            {
                var lastTag = new MockDataTags().GetAll().OrderByDescending(t => t.Added).First();
                var item = new MockData().GetAll().First(s => s.Id == lastTag.PictureId);

                return (lastTag.TagName, item.Name);
            }

            int GetUniqueCount()
            {
                return new MockDataTags().GetAll()
                    .Select(tag => tag.TagName)
                    .Distinct()
                    .Count();
            }
        }

        public async Task<AlbumMetadata> GetAlbumMetadata()
        {
            var count = Count(Types.Album);
            var mostLiked = GetMostLikedAlbum();
            var mostRecent = GetMostRecent();

            return new AlbumMetadata
            {
                Count = count,
                MostLikedCount = mostLiked.Count,
                MostLikedName = mostLiked.Name,
                MostRecentName = mostRecent.Name,
                MostRecentTimestamp = mostRecent.Timestamp
            };

            (string Name, int Count) GetMostLikedAlbum()
            {
                var dict = new Dictionary<string, int>();
                var allPics = new MockData().GetAll();
                var allLikes = new MockDataTags().GetAll().Where(w => w.TagName.ToLower() == "like");
                foreach (var tag in allLikes)
                {
                    var pic = allPics.FirstOrDefault(f => f.Id == tag.PictureId);
                    if (dict.ContainsKey(pic.FolderName))
                    {
                        dict[pic.FolderName] = dict[pic.FolderName] + 1;
                    }
                    else
                    {
                        dict.Add(pic.FolderName, 1);
                    }
                }

                var mostLiked = dict.OrderByDescending(v => v.Value).FirstOrDefault();

                return (mostLiked.Key, mostLiked.Value);
            }

            (string Name, DateTime Timestamp) GetMostRecent()
            {
                var last = new MockData().GetAll().OrderByDescending(o => o.GlobalSortOrder).First();

                return (last.FolderName, last.CreateTimestamp);
            }
        }

        private int Count(string type)
        {
            switch (type.ToLower())
            {
                case "picture":
                case "gif":
                case "video":
                    string mediaSearchterm = GetMediaSearchTerm(type);
                    return new MockData().GetAll().Where(w => w.Name.EndsWith(mediaSearchterm)).Count();
                case "tags":
                    return new MockDataTags().GetAll().Count();
                case "album":
                    return new MockData().GetAll().GroupBy(g => g.FolderId).Count();
                default:
                    throw new ArgumentException();
            }
        }

        private (string Name, DateTime Timestamp) GetMostRecentMediaItem(string type)
        {
            string searchTerm = GetMediaSearchTerm(type);
            
            var data = new MockData().GetAll();
            var pictureDTO = data.Where(w => w.Name.EndsWith(searchTerm)).OrderByDescending(x => x.GlobalSortOrder).First();

            return (pictureDTO.Name, pictureDTO.CreateTimestamp);
        }

        private (string Name, int Count) GetMostLikedItem(string type)
        {
            var itemData = new MockData().GetAll();
            var tagData = new MockDataTags().GetAll();

            var tagGroups = tagData
                .Where(x => x.TagName.ToLower() == "like")
                .GroupBy(g => new
                {
                    g.PictureId,
                })
                .Select(grp => new
                {
                    grp.Key.PictureId,
                    Count = grp.Count(),
                })
                .OrderByDescending(grp => grp.Count);

            string mediaSearchTerm = GetMediaSearchTerm(type);
            foreach (var tagGroup in tagGroups)
            {
                var mediaItem = itemData.First(x => x.Id == tagGroup.PictureId);
                if (mediaItem.Name.EndsWith(mediaSearchTerm))
                {
                    return (mediaItem.Name, tagGroup.Count);
                }
            }

            return ("N/A", 0);
        }

        private string GetMediaSearchTerm(string type)
        {
            string searchTerm = type switch
            {
                "picture" => ".jpg",
                "gif" => ".gif",
                "video" => ".mp4",
                _ => throw new NotImplementedException(),
            };

            return searchTerm;
        }

        public async Task<int> GetGlobalSortOrderMax()
        {
            return new MockData().GetAll().Count();
        }
    }
}
