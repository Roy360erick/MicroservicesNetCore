using System;
using System.Threading.Tasks;
using MicroserviceShoppingCart.RemoteModels;

namespace MicroserviceShoppingCart.RemoteInterfaces
{
    public interface IBookService
    {
        Task<(bool Result, BookRemote Book, string ErroMessage)> GetBook(Guid? BookID);
    }
}
