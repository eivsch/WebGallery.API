﻿using System;

namespace Application.Models.Metadata
{
    public class MetadataVideoDetails : MetadataDetails
    {
        public string MostLikedName { get; set; }
        public string MostRecentName { get; set; }
        public int? MostLikedCount { get; set; }
        public DateTime? MostRecentTimestamp { get; set; }
    }
}
