using Application.Common;
using Application.Interfaces;
using Application.Services.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services
{
    public class RefDataDirectorService : HttpClientBase, IRefDataDirectorService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RefDataDirectorService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<IEnumerable<ModelObject>> GetData()
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Directors").Value;
            return (await Get<List<ModelObject>>(_httpClient, new Uri(endpoint))).Value;
        }

        public async Task<ModelObject> PostData(ModelName modelName)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Directors").Value;
            var body = JsonConvert.SerializeObject(modelName);
            return (await Post<ModelObject>(_httpClient, new Uri(endpoint), body)).Value;
        }
        
        public async Task<ModelObject> PutData(ModelObject modelObject)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Directors").Value;
            var body = JsonConvert.SerializeObject(modelObject);
            return (await Put<ModelObject>(_httpClient, new Uri(endpoint), body)).Value;
        }

        public async Task<ModelObject> DeleteData(ModelId id)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Directors").Value;
            var body = JsonConvert.SerializeObject(id);
            return (await Delete<ModelObject>(_httpClient, new Uri(endpoint), body)).Value;
        }

        public async Task<ModelObject> GetByIdData(ModelId id)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Directors").Value;
            string uriId = $"/api/Director/{id.Id}";
            return (await GetById<ModelObject>(_httpClient, new Uri(endpoint), uriId)).Value;
        }
    }
}
