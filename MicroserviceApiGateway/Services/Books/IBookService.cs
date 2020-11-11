using MicroserviceApiGateway.Models;
using System;
using System.Threading.Tasks;

namespace MicroserviceApiGateway.Services.Books
{
    public interface IBookService
    {
        Task<(bool Resultado, Book Book, string ErrorMessage)> GetBook(Guid BookID);
    }
}
