namespace Application.Models.Metadata
{
    public class MetadataTagDetails : MetadataDetails
    {
        public string MostPopularName { get; set; }
        public string MostRecentMediaName { get; set; }
        public string MostRecentTagName { get; set; }
        public int? MostPopularCount { get; set; }
    }
}
