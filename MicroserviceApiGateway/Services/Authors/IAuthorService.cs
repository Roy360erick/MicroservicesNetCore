using MicroserviceApiGateway.Models;
using System;
using System.Threading.Tasks;

namespace MicroserviceApiGateway.Services.Authors
{
    public interface IAuthorService
    {
        Task<(bool Resultado, Author Author, string ErrorMessage)> GetAuthor(Guid AuthorID);
    }
}
