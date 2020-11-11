using MicroserviceApiGateway.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroserviceApiGateway.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IHttpClientFactory httpClient,ILogger<AuthorService> logger)
        {
            _httpClient = httpClient;
           _logger = logger;
        }
        public async Task<(bool Resultado, Author Author, string ErrorMessage)> GetAuthor(Guid AuthorID)
        {
            try
            {
                var client = _httpClient.CreateClient("AuthorService");
                var response = await client.GetAsync($"/Author/{AuthorID}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resul =  JsonSerializer.Deserialize<Author>(content , options);
                    return (true, resul, null);
                }
                return (true, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (true, null, e.Message);
            }
        }
    }
}
