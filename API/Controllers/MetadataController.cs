using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> Get(string type)
        {
            Log.Information("BEGIN - MetadataController|GET");
            var a = new
            {
                ShortDescription = "Name",
                Metrics = new Dictionary<string, string>
                {
                    { "count", "1232" },
                    { "mostRecentName", "sdf.jpg" },
                    { "mostRecentTs", DateTime.Now.ToShortDateString() },
                    { "mostLikedName", "fdsfds.jps" },
                    { "mostLikedCount", "3" },
                },
            };

            return Ok(a);
        }
    }
}
