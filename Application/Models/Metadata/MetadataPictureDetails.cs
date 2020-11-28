using API.Utilities;
using System;
using System.Text.Json.Serialization;

namespace Application.Models.Metadata
{
    public class MetadataPictureDetails : MetadataDetails
    {
        public string MostLikedName { get; set; }
        public string MostRecentName { get; set; }
        public int? MostLikedCount { get; set; }
        public DateTime? MostRecentTimestamp { get; set; }
    }
}
