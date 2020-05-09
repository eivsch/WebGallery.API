using System;
using System.Threading.Tasks;
using Application.Pictures;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PicturesController(IPictureService pictureService)
        {
            _pictureService = pictureService ?? throw new ArgumentNullException(nameof(pictureService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pictureResponse = await _pictureService.Get(id);

            return Ok(pictureResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string galleryId, int offset)
        {
            var pics = await _pictureService.GetPictures(galleryId, offset);

            return Ok(pics);
        }

        //[HttpGet]
        //public IActionResult Get(string path)
        //{
        //    try
        //    {
        //        var file = PhysicalFile(path, "image/jpeg");

        //        return file;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO: Log
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Post(PictureRequest pictureRequest)
        {
            var response = await _pictureService.Add(pictureRequest);

            return Ok(response);
        }
    }
}