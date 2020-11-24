namespace Application.Models.Metadata
{
    public class MetadataResponse
    {
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public MetadataDetails Details { get; set; }
    }
}
