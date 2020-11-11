using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MicroserviceShoppingCart.RemoteInterfaces;
using MicroserviceShoppingCart.RemoteModels;
using Microsoft.Extensions.Logging;

namespace MicroserviceShoppingCart.RemoteService
{
    public class BookService : IBookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly IHttpClientFactory _httpClient;

        public BookService(ILogger<BookService> logger, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<(bool Result, BookRemote Book, string ErroMessage)> GetBook(Guid? BookID)
        {
            try
            {
                var client = _httpClient.CreateClient("Books");
                var response = await client.GetAsync($"api/book/{BookID}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var obj = JsonSerializer.Deserialize<BookRemote>(content,options);
                    return (true, obj, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
