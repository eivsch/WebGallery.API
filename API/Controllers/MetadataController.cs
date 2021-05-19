using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Serilog;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly IMetadataService _metadataService;

        public MetadataController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string type)
        {
            var data = await _metadataService.Get(type);

            return Ok(data);
        }

        [HttpGet("global-max")]
        public async Task<IActionResult> Get()
        {
            var data = await _metadataService.GetGlobalIndexMax();

            return Ok(data);
        }
    }
}
