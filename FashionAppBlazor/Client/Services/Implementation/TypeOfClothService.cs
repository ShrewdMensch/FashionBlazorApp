using Application.DTOs;
using Application.InputModels;
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
    public class TypeOfClothService : ITypeOfClothService
    {
        private readonly HttpClient _httpClient;

        public TypeOfClothService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TypeOfClothDto> CreateOrUpdateTypeOfCloth(TypeOfClothInputModel typeOfClothInput)
        {
            var content = JsonConvert.SerializeObject(typeOfClothInput);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/typeOfClothes/upsert", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TypeOfClothDto>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task DeleteTypeOfCloth(Guid id)
        {
            await _httpClient.DeleteAsync($"api/typeOfClothes/delete/{id}");
        }

        public async Task DeleteAllTypeOfCloths()
        {
            await _httpClient.DeleteAsync($"api/typeOfClothes/deleteAll");
        }

        public Task<IEnumerable<TypeOfClothDto>> GetTypeOfCloths()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<TypeOfClothDto>>("api/typeOfClothes/all");
        }

        public Task<TypeOfClothDto> GetTypeOfCloth(Guid id)
        {
            return _httpClient.GetFromJsonAsync<TypeOfClothDto>($"api/typeOfClothes/get/{id}");
        }
    }
}
