using Application.Services.Interfaces;
using DomainModel.Aggregates.Picture.Interfaces;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IPictureRepository _pictureRepository;

        public ImageService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<string> Get(string id)
        {
            var aggregate = await _pictureRepository.FindById(id);

            return aggregate.AppPath;
        }

        public async Task<string> Get(int index)
        {
            var aggregate = await _pictureRepository.FindByIndex(index);

            return aggregate.AppPath;
        }
    }
}
