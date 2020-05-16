using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Application.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var allTags = await _tagService.GetAllUniqueTags();

            return allTags;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TagRequest tagRequest)
        {
            await _tagService.AddTag(tagRequest);

            return Ok();
        }
    }
}