using System.Threading.Tasks;
using Application.DTOs;

namespace Application.PhotoAccessor
{
    public interface IFileUpload
    {
        Task<string> UploadFile(FileDataDto file);
        bool DeleteFile(string fileName);
    }
}
