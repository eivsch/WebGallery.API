namespace Infrastructure.Pictures.DTO.ElasticSearch
{
    internal class PictureDTO
    {
        public string Name { get; set; }
        public string AppPath { get; set; }
        public int GlobalSortOrder { get; set; }
    }
}
