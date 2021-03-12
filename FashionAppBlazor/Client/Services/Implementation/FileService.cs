using Application.DTOs;
using FashionAppBlazor.Client.Services.Contract;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeletePhoto(string photoUrl)
        {
            await _httpClient.DeleteAsync($"api/file/delete/{photoUrl}");
        }

        public async Task<string> UploadPhoto(FileDataDto file)
        {
            /*var content = JsonConvert.SerializeObject(file);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");*/
            var response = await _httpClient.PostAsJsonAsync<FileDataDto>("api/file/upload", file);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                //var result = JsonConvert.DeserializeObject<string>(contentTemp);
                return contentTemp;
            }

            return null;
        }
    }
}
