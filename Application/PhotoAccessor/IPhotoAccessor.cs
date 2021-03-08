using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.PhotoAccessor
{
    public interface IPhotoAccessor
    {
        string AddPhoto(AppUser user, IFormFileCollection files);
        void AddOrUpdatePhotos(ReadyToWear readyToWear, IFormFileCollection files);
        void DeletePhotosFromServer(ReadyToWear readyToWear);
        void DeletePhotosFromServer(IEnumerable<ReadyToWear> readyToWears);
    }
}