using ImageDownloadApp.DTOs.Requests;
using ImageDownloadApp.DTOs.Responses;
using ImageDownloadApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageDownloadApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
           _imageService = imageService;
        }

        [HttpPost]
        [Route("download-images")]
        public async Task<ResponseDownload> DownloadImags(RequestDownload request)
        {
            var response = new ResponseDownload() { Success = false};

            if (!ModelState.IsValid)
            {
                response.Success = false;
                response.Message = "Invalid request!";
            }
            else
            {
                var result = await _imageService.DownloadImageAsync(request.ImageUrls.ToList(), request.MaxDownloadAtOnce);

                response.Success = true;
                response.UrlAndNames = result;
            }
            

            return response;
        }

        [HttpGet]
        [Route("get-image-by-name/{imageName}")]
        public async Task<string> GetImageByName(string imageName)
        {

            var result = await _imageService.GetBase64ImageByName(imageName);

            return result;
        }
    }
}
