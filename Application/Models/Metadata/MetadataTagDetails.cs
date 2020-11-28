namespace Application.Models.Metadata
{
    public class MetadataTagDetails : MetadataDetails
    {
        public int TotalUnique { get; set; }
        public string MostPopularName { get; set; }
        public int? MostPopularCount { get; set; }
        public string MostRecentMediaName { get; set; }
        public string MostRecentTagName { get; set; }
    }
}
