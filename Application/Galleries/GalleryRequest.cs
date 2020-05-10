using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Galleries
{
    public class GalleryRequest
    {
        public string Id { get; set; }
        public string FolderId { get; set; }
        public IEnumerable<GalleryPicture> GalleryPictures { get; set; }
    }
}
