using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Galleries;
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
        public async Task<IActionResult> Get(int itemCount = 24)
        {
            Log.Information("BEGIN - GalleryController|GET");
            var galleryResponse = await _galleryService.GetAll();

            return Ok(galleryResponse);
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom(int itemsInEach = 24)
        {
            Log.Information("BEGIN - GalleryController|GET");
            var galleryResponse = await _galleryService.GetRandom(itemsInEach);

            return Ok(galleryResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Save(GalleryRequest request)
        {
            Log.Information("BEGIN - GalleryController|POST");
            var galleryResponse = await _galleryService.Save(request);

            return Ok(galleryResponse);
        }
    }
}
