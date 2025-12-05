/*using System;
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

*/
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/uploadfile")]
    [ApiController]
    [Authorize]
    public class UploadFileController : ControllerBase
    {
        private readonly ILogger<UploadFileController> _logger;

        public UploadFileController(ILogger<UploadFileController> logger)
        {
            _logger = logger;
        }

        [HttpPost("singlefile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                // Validate the file
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { error = "No file uploaded or file is empty." });
                }
                const long maxFileSize = 10 * 1024 * 1024; // 10MB
                if (file.Length > maxFileSize)
                {
                    return BadRequest(new { error = $"File size exceeds the limit of {maxFileSize / (1024 * 1024)}MB." });
                }
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".txt" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest(new { error = "Invalid file type. Allowed types: " + string.Join(", ", allowedExtensions) });
                }
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagesUpload");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string fileName = $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}{fileExtension}";
                string filePath = Path.Combine(uploadsFolder, fileName);
                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    fileName = $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_{counter}{fileExtension}";
                    filePath = Path.Combine(uploadsFolder, fileName);
                    counter++;
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                string fileUrl = $"{Request.Scheme}://{Request.Host}/imagesUpload/{fileName}";

                _logger.LogInformation($"File uploaded successfully: {fileName}");

                return Ok(new
                {
                    fileUrl,
                    fileName,
                    fileSize = file.Length,
                    contentType = file.ContentType,
                    uploadedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                return StatusCode(500, new { error = "An error occurred while uploading the file.", details = ex.Message });
            }
        }
    }
}
