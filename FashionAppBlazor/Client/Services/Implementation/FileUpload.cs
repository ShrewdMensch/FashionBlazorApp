/*using Application.PhotoAccessor;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Implementation
{
    public class FileUpload: IFileUpload
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
                //var path = $"{_webHostEnvironment.WebRootPath}\\ReadyToWearImages\\{fileName}";
                var path =Path.Combine(_webHostEnvironment.WebRootPath, fileName);

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

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                var folderName = "ReadyToWearImages";
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\{folderName}";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
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
*/