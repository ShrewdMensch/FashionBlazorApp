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
    public class IncurredExpenseService : IIncurredExpenseService
    {
        private readonly HttpClient _httpClient;

        public IncurredExpenseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IncurredExpenseDto> CreateOrUpdateIncurredExpense(IncurredExpenseDto incurredExpense)
        {
            var content = JsonConvert.SerializeObject(incurredExpense);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/incurredExpenses/upsert", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IncurredExpenseDto>(contentTemp);
                return result;
            }

            return null;
        }

        public async Task DeleteIncurredExpense(Guid id)
        {
            await _httpClient.DeleteAsync($"api/incurredExpenses/delete/{id}");
        }

        public async Task DeleteAllIncurredExpenses()
        {
            await _httpClient.DeleteAsync($"api/incurredExpenses/deleteAll");
        }

        public Task<IEnumerable<IncurredExpenseDto>> GetIncurredExpenses()
        {
            return _httpClient.GetFromJsonAsync<IEnumerable<IncurredExpenseDto>>("api/incurredExpenses/all");
        }

        public Task<IncurredExpenseDto> GetIncurredExpense(Guid id)
        {
            return _httpClient.GetFromJsonAsync<IncurredExpenseDto>($"api/incurredExpenses/get/{id}");
        }
    }
}
