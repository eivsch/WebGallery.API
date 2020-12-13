using Application.Common.Interfaces;
using System.Collections.Generic;

namespace Application.Galleries
{
    public class GalleryResponse : IServiceResponse
    {
        public string Id { get; set; }
        public string GalleryName { get; set; }
        public int ImageCount { get; set; }
        public IEnumerable<GalleryPicture> GalleryPictures { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
