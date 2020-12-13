namespace Infrastructure.Galleries.DTO
{
    internal class GalleryPictureDTO
    {
        public string Id { get; set; }
        public int GlobalSortOrder { get; set; }
        public string Name { get; set; }
        public string FolderId { get; set; }
        public string AppPath { get; set; }
        public int FolderSortOrder { get; set; }
    }
}
