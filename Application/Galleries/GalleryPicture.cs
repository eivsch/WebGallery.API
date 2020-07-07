using System.Collections.Generic;

namespace Application.Galleries
{
    public class GalleryPicture
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
