using Application.DTOs;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface IFileService
    {
        public Task<string> UploadPhoto(FileDataDto file);
        public Task DeletePhoto(string photoUrl);
    }
}
