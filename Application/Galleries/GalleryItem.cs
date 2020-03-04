using System.Collections.Generic;

namespace Application.Galleries
{
    public class GalleryItem
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
