using DomainModel.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Picture.Interfaces
{
    public interface IPictureRepository : IRepository<Picture>
    {
        Task<Picture> FindByIndex(int i);
        Task<Picture> FindByGalleryIndex(string galleryId, int index);
        Task<IEnumerable<Picture>> FindAll(string galleryId, int offset = 0);
        Task<Picture> FindByAppPath(string appPath);
    }
}
