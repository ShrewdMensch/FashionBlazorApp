using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface IAccessoryService
    {
        public Task<AccessoryDto> GetAccessory(Guid id);
        public Task<IEnumerable<AccessoryDto>> GetAccessories();
        public Task<IEnumerable<AccessoryWithQuantityDto>> GetAccessoriesForForm();
        public Task<AccessoryDto> CreateOrUpdateAccessory(AccessoryDto accessoryDto);
        public Task DeleteAccessory(Guid id);
        public Task DeleteAllAccessories();
    }
}
