using System;

namespace Infrastructure.Pictures.DTO.ElasticSearch
{
    internal class PictureDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AppPath { get; set; }
        public string OriginalPath { get; set; }
        public string FolderName { get; set; }
        public string FolderId { get; set; }
        public int FolderSortOrder { get; set; }
        public int GlobalSortOrder { get; set; }
        public int Size { get; set; }
        public DateTime? CreateTimestamp { get; set; }
    }
}
