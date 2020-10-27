using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> Get(string id);
        Task<string> Get(int index);
    }
}
