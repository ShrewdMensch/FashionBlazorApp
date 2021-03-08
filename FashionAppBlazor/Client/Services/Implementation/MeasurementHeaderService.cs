using Application.DTOs;
using FashionAppBlazor.Client.Services.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Implementation
{
    public class MeasurementHeaderService : IMeasurementHeaderService
    {
        private readonly HttpClient _httpClient;

        public MeasurementHeaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EntityDto> CreateOrUpdateMeasurementHeader(EntityDto entityDto)
        {
            var content = JsonConvert.SerializeObject(entityDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/measurementHeaders/upsert", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<EntityDto>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task DeleteMeasurementHeader(Guid id)
        {
            await _httpClient.DeleteAsync($"api/measurementHeaders/delete/{id}");
        }

        public async Task DeleteAllMeasurementHeaders()
        {
            await _httpClient.DeleteAsync($"api/measurementHeaders/deleteAll");
        }

        public Task<IEnumerable<EntityDto>> GetMeasurementHeaders()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<EntityDto>>("api/measurementHeaders/all");
        }

        public Task<EntityDto> GetMeasurementHeader(Guid id)
        {
            return _httpClient.GetFromJsonAsync<EntityDto>($"api/measurementHeaders/get/{id}");
        }
    }
}
