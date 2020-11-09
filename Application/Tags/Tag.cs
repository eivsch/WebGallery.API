using System.Collections.Generic;

namespace Application.Tags
{
    public class Tag
    {
        public string Name { get; set; }
        public int ItemCount { get; set; }
        public IEnumerable<TagMediaItem> MediaItems { get; set; }
    }
}
