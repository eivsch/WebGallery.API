using System;

namespace Infrastructure.Services.DTO
{
    internal class ItemDTO
    {
        public string Id { get; set; }
        public int GlobalSortOrder { get; set; }
        public string Name { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public string FolderId { get; set; }
    }
}
