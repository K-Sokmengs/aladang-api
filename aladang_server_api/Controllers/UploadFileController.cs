using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/uploadfile")]
    [ApiController, Authorize]
    public class UploadFileController : ControllerBase
    {
        [HttpPost("singlefile")]
        public IActionResult UploadFile(IFormFile file)
        {
            string fileUrl = "";
            try
            {
                if (file != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "imagesUpload");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string nameFile = string.Concat(
                        DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"),
                        Path.GetExtension(file.FileName)
                    );

                    string filePath = Path.Combine(uploadsFolder, nameFile);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    fileUrl = $"{Request.Scheme}://{Request.Host}/imagesUpload/{nameFile}";
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while uploading the file.");
            }

            return Ok(new { fileUrl });
        }
    }
}

