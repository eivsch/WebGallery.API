using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Application.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        public async Task<IEnumerable<Tag>> Get()
        {
            var allTags = await _tagService.GetAllUniqueTags();

            return allTags;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tag tagRequest)
        {
            try
            {
                await _tagService.AddTag(tagRequest);
            }
            catch (ApplicationException appEx)
            {
                Log.Error(appEx.Message);

                return NotFound();
            }

            return Ok();
        }
    }
}