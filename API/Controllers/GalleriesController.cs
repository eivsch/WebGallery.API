using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleriesController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleriesController(IGalleryService galleryService)
        {
            _galleryService = galleryService ?? throw new ArgumentNullException(nameof(galleryService));
        }

        [HttpGet]
        public async Task<ActionResult> Get(int itemCount = 24)
        {
            Log.Information("BEGIN - GalleryController|GET");
            var galleryResponse = await _galleryService.GetAll();

            return Ok(galleryResponse);
        }

        [HttpGet("random")]
        public async Task<ActionResult> GetRandom(int num = 12, int itemsInEach = 24)
        {
            Log.Information("BEGIN - GalleryController|GET");
            var galleryResponse = await _galleryService.GetRandom(num, itemsInEach);

            return Ok(galleryResponse);
        }
    }
}
