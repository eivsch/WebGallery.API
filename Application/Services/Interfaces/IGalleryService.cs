﻿using Application.Common.Interfaces;
using Application.Galleries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IGalleryService : IApplicationService<GalleryResponse>
    {
        Task<GalleryResponse> Get(string id, int itemIndexStart, int numberOfItems);
        Task<IEnumerable<GalleryResponse>> GetAll();
        Task<GalleryResponse> GetCustomizedRandom(int itemsInGallery, string tags, string tagFilteringMode, string mediaFilterMode);
    }
}
