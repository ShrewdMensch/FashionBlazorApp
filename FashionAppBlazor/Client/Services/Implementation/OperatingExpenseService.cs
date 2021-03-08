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
    public class OperatingExpenseService : IOperatingExpenseService
    {
        private readonly HttpClient _httpClient;

        public OperatingExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OperatingExpenseDto> CreateOrUpdateOperatingExpense(OperatingExpenseInputModel operatingExpenseInput)
        {
            var content = JsonConvert.SerializeObject(operatingExpenseInput);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/operatingExpenses/upsert", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperatingExpenseDto>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task DeleteOperatingExpense(Guid id)
        {
            await _httpClient.DeleteAsync($"api/operatingExpenses/delete/{id}");
        }

        public async Task DeleteAllOperatingExpenses()
        {
            await _httpClient.DeleteAsync($"api/operatingExpenses/deleteAll");
        }

        public Task<IEnumerable<OperatingExpenseDto>> GetOperatingExpenses()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<OperatingExpenseDto>>("api/operatingExpenses/all");
        }

        public Task<OperatingExpenseDto> GetOperatingExpense(Guid id)
        {
            return _httpClient.GetFromJsonAsync<OperatingExpenseDto>($"api/operatingExpenses/get/{id}");
        }
    }
}
