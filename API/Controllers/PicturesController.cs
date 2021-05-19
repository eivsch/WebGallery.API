using System;
using System.Threading.Tasks;
using Application.Pictures;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PicturesController(IPictureService pictureService)
        {
            _pictureService = pictureService ?? throw new ArgumentNullException(nameof(pictureService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pictureResponse = await _pictureService.Get(id);

            return Ok(pictureResponse);
        }

        [HttpGet("sha/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var pictureResponse = await _pictureService.Get(id);

            return Ok(pictureResponse);
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom(string albumId)
        {
            PictureResponse pic;
            if (!string.IsNullOrWhiteSpace(albumId))
                pic = await _pictureService.GetRandomFromAlbum(albumId);
            else 
                pic = await _pictureService.Get(-1);

            if (pic is null)
                return NotFound();

            return Ok(pic);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PictureRequest pictureRequest)
        {
            var response = await _pictureService.Add(pictureRequest);

            return Ok(response);
        }
    }
}