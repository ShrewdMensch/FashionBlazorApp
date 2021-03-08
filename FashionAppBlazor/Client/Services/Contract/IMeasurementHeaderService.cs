using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface IMeasurementHeaderService
    {
        public Task<EntityDto> GetMeasurementHeader(Guid id);
        public Task<IEnumerable<EntityDto>> GetMeasurementHeaders();
        public Task<EntityDto> CreateOrUpdateMeasurementHeader(EntityDto entityDto);
        public Task DeleteMeasurementHeader(Guid id);
        public Task DeleteAllMeasurementHeaders();
    }
}
