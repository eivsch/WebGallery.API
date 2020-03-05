using Application.Pictures;
using Application.Services.Interfaces;
using DomainModel.Aggregates.Picture.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository ?? throw new ArgumentNullException(nameof(pictureRepository));
        }

        public async Task<PictureResponse> Get(string id)
        {
            var pic = await _pictureRepository.FindById(id);

            return new PictureResponse
            {
                Id = pic.Id,
                Path = pic.FileSystemPath
            };
        }
    }
}
