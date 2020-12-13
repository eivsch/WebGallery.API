using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Galleries.DTO
{
    internal class GalleryDTO
    {
        public string Id { get; set; }
        public string FolderId { get; set; }
        public string FolderName { get; set; }
        public IEnumerable<GalleryPictureDTO> GalleryPictures { get; set; }
    }
}
