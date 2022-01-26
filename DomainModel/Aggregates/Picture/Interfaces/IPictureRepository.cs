using DomainModel.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Picture.Interfaces
{
    public interface IPictureRepository : IRepository<Picture>
    {
        Task<Picture> FindByIndex(int i);
        Task<Picture> FindByAppPath(string appPath);
        Task<Picture> FindRandomFromAlbum(string albumId);
        Task<IEnumerable<Picture>> Search(string query);
    }
}
