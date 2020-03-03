using Application.Common.Interfaces;
using Application.Gallery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    interface IGalleryService : IApplicationService<GalleryResponse>
    {
        Task<GalleryResponse> Get(int numberOfItems);
        Task<GalleryResponse> Get(GalleryRequest galleryRequest);
    }
}
