﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices;
using Newtonsoft.Json;
using xingyi.microservices.repository;

namespace xingyi.microservices.Client
{
    public interface IApiClient<T, Id, Where> : IRepository<T, Id, Where>
        where T : class
        where Where : IRepositoryWhere<T>

    {
    }

    public class HttpClientSettings
    {
        public string BaseUrl { get; set; }
    }


    public class GenericClient<T, Id, Where> : IApiClient<T, Id, Where>
        where T : class
        where Where : IRepositoryWhere<T>
    {
        public readonly HttpClient _httpClient;
        public readonly string _baseUrl; 
        public GenericClient(HttpClient httpClient, string BaseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = BaseUrl;
        }

        private String addEagerLoad(string url, bool eagerLoad)
        {
            return $"{url}?eagerLoad={eagerLoad}";
        }
        private String addEagerLoadAndWhere(string url, bool eagerLoad, Where where)
        {
            var l = addEagerLoad(url, eagerLoad);
            var q = where.queryString();
            var result = q == "" ? l : $"{l}&{q}";
            return result;
        }
        public async Task<List<T>> GetAllAsync(Where where, Boolean eagerLoad = false)
        {
            var url = addEagerLoadAndWhere(_baseUrl, eagerLoad, where);

            var response = await _httpClient.GetAsync(url);
            Console.WriteLine(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public async Task<T> GetByIdAsync(Id id, Boolean eagerLoad = true)
        {
            var response = await _httpClient.GetAsync(addEagerLoad($"{_baseUrl}/{id}", eagerLoad));
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

        public async Task<bool> DeleteAsync(Id id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}


