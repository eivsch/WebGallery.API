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
            _galleryService = galleryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int itemCount = 24)
        {
            var galleryResponseList = await _galleryService.GetAll();

            return Ok(galleryResponseList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, int itemIndexStart = 1, int numberOfItems = 48)
        {
            var galleryResponseList = await _galleryService.Get(id, itemIndexStart, numberOfItems);

            return Ok(galleryResponseList);
        }

        [HttpGet("customized-random")]
        public async Task<IActionResult> GetCustomRandom(int itemsInEach = 12, string tags = "", string tagFilterMode = "", string mediaFilterMode = "include")
        {
            if (string.IsNullOrWhiteSpace(tags))
            {
                if (string.IsNullOrWhiteSpace(tagFilterMode))
                    tagFilterMode = "undefined";
                else if (tagFilterMode == "custominclusive" || tagFilterMode == "customexclusive")
                    return BadRequest("Illegal tag filter mode - Need to provide tags to filter");
            }
            else if (string.IsNullOrWhiteSpace(tagFilterMode))
                    tagFilterMode = "custominclusive";

            var galleryResponse = await _galleryService.GetCustomizedRandom(itemsInEach, tags, tagFilterMode, mediaFilterMode);

            return Ok(galleryResponse);
        }
    }
}
