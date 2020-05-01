using DomainModel.Common.Interfaces;
using System.Threading.Tasks;

namespace DomainModel.Aggregates.Picture.Interfaces
{
    public interface IPictureRepository : IRepository<Picture>
    {
        Task<string> FindByIndex(int i);
    }
}
