using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService ?? throw new ArgumentNullException(nameof(galleryService));
        }

        [HttpGet]
        public async Task<ActionResult> Get(int itemCount = 24)
        {
            var galleryResponse = await _galleryService.Generate(itemCount);

            return Ok(galleryResponse);
        }
    }
}
