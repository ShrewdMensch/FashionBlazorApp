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
    public class ReadyToWearService : IReadyToWearService
    {
        private readonly HttpClient _httpClient;

        public ReadyToWearService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReadyToWearDto> CreateOrUpdateReadyToWear(ReadyToWearDto readyToWearDto)
        {
            var content = JsonConvert.SerializeObject(readyToWearDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/readyToWears/upsert", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ReadyToWearDto>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task DeleteReadyToWear(Guid id)
        {
            await _httpClient.DeleteAsync($"api/readyToWears/delete/{id}");
        }

        public async Task DeleteAllReadyToWears()
        {
            await _httpClient.DeleteAsync($"api/readyToWears/deleteAll");
        }

        public Task<IEnumerable<ReadyToWearDto>> GetReadyToWears()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<ReadyToWearDto>>("api/readyToWears/all");
        }

        public Task<ReadyToWearDto> GetReadyToWear(Guid id)
        {
            return _httpClient.GetFromJsonAsync<ReadyToWearDto>($"api/readyToWears/get/{id}");
        }

        public async Task<ReadyToWearPhotoDto> CreatePhoto(ReadyToWearPhotoDto readyToWearPhoto)
        {
            var content = JsonConvert.SerializeObject(readyToWearPhoto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/readyToWearPhotos/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ReadyToWearPhotoDto>(contentTemp);
                return result;
            }

            return null;
        }

        public Task DeletePhotoById(Guid id)
        {
            return _httpClient.DeleteAsync($"api/readyToWearPhotos/deleteById/{id}");
        }

        public Task DeletePhotoByUrl(string photoUrl)
        {
            return _httpClient.DeleteAsync($"api/readyToWearPhotos/deleteByUrl/{photoUrl}");
        }
    }
}
