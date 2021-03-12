using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface IReadyToWearService
    {
        public Task<ReadyToWearDto> GetReadyToWear(Guid id);
        public Task<IEnumerable<ReadyToWearDto>> GetReadyToWears();
        public Task<ReadyToWearDto> CreateOrUpdateReadyToWear(ReadyToWearDto accessoryDto);
        public Task DeleteReadyToWear(Guid id);
        public Task DeleteAllReadyToWears();
        public Task<ReadyToWearPhotoDto> CreatePhoto(ReadyToWearPhotoDto readyToWearPhoto);
        public Task DeletePhotoById(Guid id);
        public Task DeletePhotoByUrl(string photoUrl);
    }
}
