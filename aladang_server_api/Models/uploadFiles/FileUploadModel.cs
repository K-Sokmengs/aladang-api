using System;
using Microsoft.VisualBasic.FileIO;

namespace aladang_server_api.Models.uploadFiles
{
	public class FileUploadModel
	{
        public IFormFile? FileDetails { get; set; }
        //Public FileType FileType { get; set; }
    }


    public enum FileType
    {
        PDF = 1,
        DOCX = 2,
        JPEG=3,
        PNG=4
    }
}

