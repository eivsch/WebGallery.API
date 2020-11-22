namespace Application.Models.Metadata
{
    public class MetadataResponse
    {
        public string ShortDescription { get; set; }
        public int TotalCount { get; set; }
        public IMetadataDetails Details { get; set; }
    }
}
