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
    public class AccessoryService : IAccessoryService
    {
        private readonly HttpClient _httpClient;

        public AccessoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AccessoryDto> CreateOrUpdateAccessory(AccessoryDto accessoryDto)
        {
            var content = JsonConvert.SerializeObject(accessoryDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/accessories/upsert", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AccessoryDto>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task DeleteAccessory(Guid id)
        {
            await _httpClient.DeleteAsync($"api/accessories/delete/{id}");
        }

        public async Task DeleteAllAccessories()
        {
            await _httpClient.DeleteAsync($"api/accessories/deleteAll");
        }

        public Task<IEnumerable<AccessoryDto>> GetAccessories()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<AccessoryDto>>("api/accessories/all");
        }
        
        public Task<IEnumerable<AccessoryWithQuantityDto>> GetAccessoriesForForm()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<AccessoryWithQuantityDto>>("api/accessories/allForForm");
        }

        public Task<AccessoryDto> GetAccessory(Guid id)
        {
            return _httpClient.GetFromJsonAsync<AccessoryDto>($"api/accessories/get/{id}");
        }
    }
}
