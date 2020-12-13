using System.Collections.Generic;

namespace Application.Galleries
{
    public class GalleryPicture
    {
        public string Id { get; set; }
        public string MediaType { get; set; }
        public string AppPath { get; set; }
        public int IndexGlobal { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
