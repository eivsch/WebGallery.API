using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Gallery
{
    public class GalleryResponse : IServiceResponse
    {
        public List<GalleryItem> GalleryItems { get; set; }
    }
}
