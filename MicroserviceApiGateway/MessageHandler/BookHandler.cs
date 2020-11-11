using MicroserviceApiGateway.Models;
using MicroserviceApiGateway.Services.Authors;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceApiGateway.MessageHandler
{
    public class BookHandler : DelegatingHandler
    {
        private readonly ILogger<BookHandler> _logger;
        private readonly IAuthorService _authorService;

        public BookHandler(ILogger<BookHandler> logger,IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            var time = Stopwatch.StartNew();

            _logger.LogInformation("Incia el request");

            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var result = JsonSerializer.Deserialize<Book>(content, options);
                var author = await _authorService.GetAuthor(result.AuthorGuid ?? Guid.Empty);
                if (author.Resultado)
                {
                    result.Author = author.Author;
                    response.Content = new StringContent(JsonSerializer.Serialize(result),System.Text.Encoding.UTF8,"application/json");
                }

            }

            _logger.LogInformation($"Este proceso se hizo en {time.ElapsedMilliseconds}ms");

            return response;
        }
    }
}
