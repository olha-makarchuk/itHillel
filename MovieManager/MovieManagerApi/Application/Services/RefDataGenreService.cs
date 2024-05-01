using Application.Common;
using Application.Interfaces;
using Application.Services.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services
{
    public class RefDataGenreService : HttpClientBase, IRefDataGenreService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;

        public RefDataGenreService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<IEnumerable<ModelObject>> GetData()
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Genres").Value;
            return (await Get<List<ModelObject>>(_httpClient, new Uri(endpoint))).Value;
        }

        public async Task<ModelObject> PostData(ModelName modelName)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Genres").Value;
            var body = JsonConvert.SerializeObject(modelName);
            return (await Post<ModelObject>(_httpClient, new Uri(endpoint), body)).Value;
        }

        public async Task<ModelObject> PutData(ModelObject modelObject)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Genres").Value;
            var body = JsonConvert.SerializeObject(modelObject);
            return (await Put<ModelObject>(_httpClient, new Uri(endpoint), body)).Value;
        }

        public async Task<ModelObject> DeleteData(ModelId id)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Genres").Value;
            var body = JsonConvert.SerializeObject(id);
            return (await Delete<ModelObject>(_httpClient, new Uri(endpoint), body)).Value;
        }

        public async Task<ModelObject> GetByIdData(ModelId id)
        {
            var endpoint = _configuration.GetSection("RefDataEndpoints:Genres").Value;
            string uriId = $"/api/Genre/{id.Id}";
            return (await GetById<ModelObject>(_httpClient, new Uri(endpoint), uriId)).Value;
        }
    }
}
