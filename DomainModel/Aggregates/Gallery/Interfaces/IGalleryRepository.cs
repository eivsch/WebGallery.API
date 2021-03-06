﻿using DomainModel.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Gallery.Interfaces
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        Task<Gallery> FillEmptyGalleryWithItems(Gallery gallery);
        Task<List<Gallery>> GetAll();
        Task<Gallery> GetRandom(int itemsInGallery);
    }
}
