using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private string _test;

        public PingController(IConfiguration configuration)
        {
            _test = configuration.GetValue($"ConnectionStrings:ElasticSearchEndpoint", "default");
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(_test);
        }
    }
}