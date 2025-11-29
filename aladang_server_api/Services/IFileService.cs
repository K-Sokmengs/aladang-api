using System;
using aladang_server_api.Models.uploadFiles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using aladang_server_api.Models;

namespace aladang_server_api.Services
{
    public interface IFileService
    {
        //Public Task PostFileAsync(IFormFile fileData, FileType fileType);
        public Task PostFileAsync(IFormFile fileData);
        public Task PostMultiFileAsync(List<FileUploadModel> fileData);

        //Public Task DownloadFileById(int fileName);
    }

    public class FileService : IFileService
    {
        //private readonly IWebHostEnvironment webHostEnvironment;
        //private readonly IConfiguration _configuration;
        //IConfiguration configuration, 
        //Public FileService(IWebHostEnvironment hostEnvironment)
        //{
        //    webHostEnvironment = hostEnvironment;
        //    //_configuration = configuration;
        //}

        //Public Task DownloadFileById(int fileName)
        //{
        //    throw new NotImplementedException();
        //}



        public Task PostMultiFileAsync(List<FileUploadModel> fileData)
        {
            try
            {
                string? uniqueFileName = null;
                foreach (FileUploadModel file in fileData)
                {
                    //var fileDetails = new FileDetails()
                    //{
                    //    ID = 0,
                    //    FileName = file.FileDetails.FileName,
                    //    FileType = file.FileType,
                    //};

                    //using (var stream = new MemoryStream())
                    //{
                    //    file.FileDetails.CopyTo(stream);
                    //    fileDetails.FileData = stream.ToArray();
                    //}

                    //var result = dbContextClass.FileDetails.Add(fileDetails);


                    //uploadfiles
                    //string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "imagesUpload");
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "imagesUpload");
                    string photoName = Path.Combine(
                             Path.GetDirectoryName(file.FileDetails!.FileName)!
                              , string.Concat(
                                 DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_")
                                 , Path.GetExtension(file.FileDetails!.FileName)

                             ));
                    //model.photo.FileName

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photoName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                }
                //await dbContextClass.SaveChangesAsync();


            }
            catch (Exception)
            {
                throw;
            }

            return Task.CompletedTask;
        }

        public Task PostFileAsync(IFormFile fileData)
        {
            try
            {
                //var fileDetails = new FileDetails()
                //{
                //    ID = 0,
                //    FileName = fileData.FileName,
                //    FileType = fileType,
                //};

                //using (var stream = new MemoryStream())
                //{
                //    fileData.CopyTo(stream);
                //    fileDetails.FileData = stream.ToArray();
                //}

                //var result = dbContextClass.FileDetails.Add(fileDetails);
                //await dbContextClass.SaveChangesAsync();
                string? uniqueFileName = null;

                if (fileData != null)
                {

                //var uploadsFolder = new PhysicalFileProvider(Path.Combine(path1: webHostEnvironment.ContentRootPath, "imagesUpload"));

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "imagesUpload");
                //uploadfiles
                //string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "imagesUpload");
                    string photoName = Path.Combine(
                             Path.GetDirectoryName(fileData.FileName)!
                              , string.Concat(
                                 DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_")
                                 , Path.GetExtension(fileData.FileName)

                             ));
                    //model.photo.FileName

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photoName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);


                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {

                        fileData.CopyTo(fileStream);

                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Task.CompletedTask;
        }

        //private string UploadedFile(UpdatePhotoUser model)
        //{
        //    string? uniqueFileName = null;

        //    if (model.photoNew != null)
        //    {
        //        //uploadfiles
        //        //string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "imagesUpload");
        //        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "imagesUpload");
        //        string photoName = Path.Combine(
        //                 Path.GetDirectoryName(model.photoNew.FileName)!
        //                  , string.Concat(
        //                     DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss_")
        //                     , Path.GetExtension(model.photoNew.FileName)

        //                 ));
        //        //model.photo.FileName

        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + photoName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);


        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {

        //            model.photoNew.CopyTo(fileStream);

        //        }
        //    }
        //    return uniqueFileName!;
        //}

        private bool DeleteFile(string photoAddress = "")
        {
            try
            {
                if (photoAddress != null && photoAddress.Length > 0)
                {
                    //string fullPath = webHostEnvironment.WebRootPath("~" + photoAddress);
                    //string fullPath = Path.Combine(webHostEnvironment.WebRootPath, "imagesUpload", photoAddress);
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "imagesUpload", photoAddress);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}

