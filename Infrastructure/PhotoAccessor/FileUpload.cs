using Application.DTOs;
using Application.PhotoAccessor;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.PhotoAccessor
{
    public class FileUpload : IFileUpload
    {
        private readonly IHostingEnvironment _webHostEnvironment;

        public FileUpload(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\ReadyToWearImages\\{fileName}";
                //var path =Path.Combine(_webHostEnvironment.WebRootPath, fileName);

                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadFile(FileDataDto file)
        {
            try
            {
                string fileExtension = file.FileType.ToLower().Contains("png") ? ".png" : ".jpg";
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var folderName = "ReadyToWearImages";
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\{folderName}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);


                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                using (var fileStream = System.IO.File.Create(path))
                {
                    await fileStream.WriteAsync(file.Data);
                }

                var fullPath = $"{folderName}/{fileName}";

                return fullPath;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
    }
}
