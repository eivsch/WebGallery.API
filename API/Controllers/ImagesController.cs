using System.IO;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IPictureService _pictureService;
        private readonly IConfiguration _configuration;
        private readonly string _root;

        public ImagesController(IPictureService pictureService, IConfiguration configuration)
        {
            _pictureService = pictureService;
            _configuration = configuration;

            _root = _configuration.GetValue($"RootFolder", "");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pic = await _pictureService.Get(id);

            var path = GetAbsolutePath(pic.AppPath);

            return PhysicalFile(path, "image/jpeg");
        }

        [HttpGet("sha/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var pictureResponse = await _pictureService.Get(id);

            return Ok(pictureResponse);
        }

        [HttpGet("{galleryId}/{pictureId}")]
        public async Task<IActionResult> GetByGallery(string galleryId, int pictureId)
        {
            var pic = await _pictureService.Get(galleryId, pictureId);

            var path = GetAbsolutePath(pic.AppPath);

            return PhysicalFile(path, "image/jpeg");
        }

        private string GetAbsolutePath(string appPath)
        {
            if (Path.DirectorySeparatorChar == '/')
                appPath = appPath.Replace('\\', '/');

            return Path.Combine(_root, appPath);
        }
    }
}