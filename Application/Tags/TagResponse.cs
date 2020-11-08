using System.Collections.Generic;

namespace Application.Tags
{
    public class TagResponse
    {
        public string TagName { get; set; }
        public IEnumerable<TagMediaItem> MediaItems { get; set; }
    }
}
