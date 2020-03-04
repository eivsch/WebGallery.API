﻿using DomainModel.Common.Interfaces;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Gallery.Interfaces
{
    public interface IGalleryRepository : IRepository<Gallery>
    {
        Task<Gallery> GetItems(Gallery gallery);
    }
}
