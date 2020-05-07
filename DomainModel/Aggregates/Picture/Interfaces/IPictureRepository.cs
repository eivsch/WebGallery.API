using DomainModel.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Picture.Interfaces
{
    public interface IPictureRepository : IRepository<Picture>
    {
        Task<string> FindByIndex(int i);
        Task<string> FindByGalleryIndex(string galleryId, int index);
        Task<IEnumerable<Picture>> FindAll(string galleryId, int offset = 0);
    }
}
