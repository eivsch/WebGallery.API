using System.Collections;
using System.Collections.Generic;

namespace Application.Tags
{
    public class TagResponse
    {
        public string TagName { get; set; }
        IEnumerable<TaggedItem> Items { get; set; }
    }
}
