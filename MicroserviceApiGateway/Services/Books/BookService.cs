using MicroserviceApiGateway.Models;
using System;
using System.Threading.Tasks;

namespace MicroserviceApiGateway.Services.Books
{
    public class BookService : IBookService
    {
        public Task<(bool Resultado, Book Book, string ErrorMessage)> GetBook(Guid BookID)
        {
            return null;
        }
    }
}
