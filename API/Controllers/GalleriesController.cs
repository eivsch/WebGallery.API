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
        private readonly ITagService _tagService;

        public GalleriesController(IGalleryService galleryService, ITagService tagService)
        {
            _galleryService = galleryService;
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int itemCount = 24)
        {
            Log.Information("BEGIN - GalleryController|GET");
            var galleryResponse = await _galleryService.GetAll();

            return Ok(galleryResponse);
        }

        [HttpGet("customized-random")]
        public async Task<IActionResult> GetCustomRandom(int itemsInEach = 12, string tags = "", string tagFilterMode = "")
        {
            Log.Information("BEGIN - GalleryController|GET");

            if (string.IsNullOrWhiteSpace(tags))
            {
                if (string.IsNullOrWhiteSpace(tagFilterMode))
                    tagFilterMode = "undefined";
                else if (tagFilterMode == "custominclusive" || tagFilterMode == "customexclusive")
                    return BadRequest("Illegal tag filter mode - Need to provide tags to filter");
            }
            else if (string.IsNullOrWhiteSpace(tagFilterMode))
                    tagFilterMode = "custominclusive";

            var galleryResponse = await _galleryService.GetCustomizedRandom(itemsInEach, tags, tagFilterMode);

            return Ok(galleryResponse);
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTagged(string tag)
        {
            Log.Information("BEGIN - GalleryController|GET");
            var tags = await _tagService.GetAll(tag);

            return Ok(tags);
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
