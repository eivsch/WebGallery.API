using Infrastructure.Services.ServiceModels;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IMetadataService
    {
        public Task<MediaMetadata> GetPictureMetadata();
        public Task<MediaMetadata> GetGifMetadata();
        public Task<MediaMetadata> GetVideoMetadata();
        public Task<TagMetadata> GetTagMetadata();
        public Task<AlbumMetadata> GetAlbumMetadata();
        Task<int> GetGlobalSortOrderMax();
    }
}
