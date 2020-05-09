using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public ImagesController(IPictureService pictureService)
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
    }
}