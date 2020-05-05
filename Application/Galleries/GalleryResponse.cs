using Application.Common.Interfaces;
using System.Collections.Generic;

namespace Application.Galleries
{
    public class GalleryResponse : IServiceResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ImageCount { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
