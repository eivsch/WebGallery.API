namespace Infrastructure.Services.ServiceModels
{
    public class TagMetadata
    {
        public int Count { get; set; }
        public string MostPopularName { get; set; }
        public int MostPopularCount { get; set; }
        public string MostRecentMediaName { get; set; }
        public string MostRecentTagName { get; set; }
    }
}
