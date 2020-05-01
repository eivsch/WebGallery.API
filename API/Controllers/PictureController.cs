using System;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Get(int id)
        {
            var path = await _pictureService.Get(id);

            return PhysicalFile(path, "image/jpeg");
        }

        [HttpGet]
        public async Task<ActionResult> Get(string path)
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
    }
}