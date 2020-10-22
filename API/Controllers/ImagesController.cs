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
        private readonly IImageService _imageService;
        private readonly string _root;

        public ImagesController(IPictureService pictureService, IConfiguration configuration, IImageService imageService)
        {
            _pictureService = pictureService;
            _configuration = configuration;
            _imageService = imageService;

            _root = _configuration.GetValue($"RootFolder", "");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            string appPath = await _imageService.Get(id);
            var path = GetAbsolutePath(appPath);

            return PhysicalFile(path, "image/jpeg");
        }

        [HttpGet("sha/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var pictureResponse = await _pictureService.Get(id);

            return Ok(pictureResponse);
        }

        private string GetAbsolutePath(string appPath)
        {
            if (Path.DirectorySeparatorChar == '/')
                appPath = appPath.Replace('\\', '/');

            return Path.Combine(_root, appPath);
        }
    }
}