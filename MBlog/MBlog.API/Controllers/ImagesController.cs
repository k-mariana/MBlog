using MBlog.API.Models.Domain;
using MBlog.API.Models.DTO;
using MBlog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //POST upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
        {
            ValidateFileUpload(requestDto);
            if(ModelState.IsValid)
            {
                // Convert Dto to domain model
                var imageDomain = new Image
                {
                    File = requestDto.File,
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileName = requestDto.FileName,
                    FileDescription = requestDto.FileDescription
                };


                // User repository to upload image
                await imageRepository.Upload(imageDomain);

                return Ok(imageDomain);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload (ImageUploadRequestDto requestDto)
        {
            var allowedExtensions = new string[] { ".jpeg", ".jpg", ".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(requestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(requestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
