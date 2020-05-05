using System;
using System.Threading.Tasks;
using Application.Pictures;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService ?? throw new ArgumentNullException(nameof(pictureService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var path = await _pictureService.Get(id);

            return PhysicalFile(path, "image/jpeg");
        }

        [HttpGet("{galleryId}/{pictureId}")]
        public async Task<IActionResult> GetByGallery(string galleryId, int pictureId)
        {
            var path = await _pictureService.Get(galleryId, pictureId);

            return PhysicalFile(path, "image/jpeg");
        }

        [HttpGet]
        public IActionResult Get(string path)
        {
            try
            {
                var file = PhysicalFile(path, "image/jpeg");

                return file;
            }
            catch (Exception ex)
            {
                // TODO: Log
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PictureRequest pictureRequest)
        {
            var response = await _pictureService.Add(pictureRequest);

            return Ok(response);
        }
    }
}