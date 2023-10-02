using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using xingyi.microservices.repository;

namespace xingyi.job.Client
{
    public interface IApiClient<T> : IRepository<T> where T : class
    {
    }

    public class HttpClientSettings
    {
        public string BaseUrl { get; set; }
    }


    public class GenericClient<T> : IApiClient<T> where T : class 
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl; // Base URL should be provided during instantiation.

        public GenericClient(HttpClient httpClient, string BaseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = BaseUrl;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> AddAsync(T entity)
        {
            var entityJson = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, entityJson);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task UpdateAsync(T entity)
        {
            var entityJson = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_baseUrl, entityJson);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}


