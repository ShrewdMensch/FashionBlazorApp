using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface IStandardSizeService
    {
        public Task<EntityDto> GetStandardSize(Guid id);
        public Task<IEnumerable<EntityDto>> GetStandardSizes();
        public Task<EntityDto> CreateOrUpdateStandardSize(EntityDto entityDto);
        public Task DeleteStandardSize(Guid id);
        public Task DeleteAllStandardSizes();
    }
}
