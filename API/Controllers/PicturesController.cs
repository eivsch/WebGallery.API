using System;
using System.Threading.Tasks;
using Application.Pictures;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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

            return Ok(pic);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PictureRequest pictureRequest)
        {
            var response = await _pictureService.Add(pictureRequest);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var result = await _pictureService.Search(query);

            return Ok(result);
        }

        [HttpDelete("sha/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool wasDeleted = await _pictureService.DeletePicture(id);

            if (!wasDeleted)
            {
                Log.Information($"Could not delete picture {id}. It might not exist.");

                return Ok();
            }

            return Ok($"Deleted {id}");
        }
    }
}