using Application.Models.Metadata;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IMetadataService
    {
        Task<MetadataResponse> Get(string itemType);
        Task<int> GetGlobalIndexMax();
    }
}
