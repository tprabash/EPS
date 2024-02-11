using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace API.DTOs
{
    public class FileUploadResponse
    {
        public string ErrorMessage { get; set; }
        public List<FileUploadResponseData> Data { get; set; }
        // public List<IFormFile> files { get; set; }
    }
}