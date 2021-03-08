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
    public class StandardSizeService : IStandardSizeService
    {
        private readonly HttpClient _httpClient;

        public StandardSizeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EntityDto> CreateOrUpdateStandardSize(EntityDto entityDto)
        {
            var content = JsonConvert.SerializeObject(entityDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/standardSizes/upsert", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<EntityDto>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task DeleteStandardSize(Guid id)
        {
            await _httpClient.DeleteAsync($"api/standardSizes/delete/{id}");
        }

        public async Task DeleteAllStandardSizes()
        {
            await _httpClient.DeleteAsync($"api/standardSizes/deleteAll");
        }

        public Task<IEnumerable<EntityDto>> GetStandardSizes()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<EntityDto>>("api/standardSizes/all");
        }

        public Task<EntityDto> GetStandardSize(Guid id)
        {
            return _httpClient.GetFromJsonAsync<EntityDto>($"api/standardSizes/get/{id}");
        }
    }
}
