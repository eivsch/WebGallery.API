using System;

namespace Infrastructure.Services.ServiceModels
{
    public class AlbumMetadata
    {
        public int Count { get; set; }
        public string MostLikedName { get; set; }
        public int MostLikedCount { get; set; }
        public string MostRecentName { get; set; }
        public DateTime MostRecentTimestamp { get; set; }
    }
}
