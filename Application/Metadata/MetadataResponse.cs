using System.Collections.Generic;

namespace Application.Metadata
{
    public class MetadataResponse
    {
        public string ShortDescription { get; set; }
        public IReadOnlyDictionary<string, string> Metrics { get; set; }
    }
}
