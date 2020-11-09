using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroserviceShoppingCart.Persistence;
using MicroserviceShoppingCart.RemoteInterfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceShoppingCart.Application
{
    public class Query
    {
        public class RequestShoppingCart :IRequest<ShoppingCartDto>
        {
            public int ShoppingCartId { get; set; }
        }

        public class Driver : IRequestHandler<RequestShoppingCart, ShoppingCartDto>
        {
            private readonly DbContextCart _dbContext;
            private readonly IBookService _bookService;

            public Driver(DbContextCart dbContext,IBookService bookService)
            {
                _dbContext = dbContext;
                _bookService = bookService;
            }
            public async Task<ShoppingCartDto> Handle(RequestShoppingCart request, CancellationToken cancellationToken)
            {
                List<ShoppingCartDetailDto> shoppingCartDetailDtos = new List<ShoppingCartDetailDto>();

                var sessionCart = await _dbContext.SessionCarts.FirstOrDefaultAsync(x => x.SessionCartID == request.ShoppingCartId);

                var sessionCartDetails = await _dbContext.SessionCartDetails.Where(x => x.SessionCartID == request.ShoppingCartId).ToListAsync();


                foreach (var item in sessionCartDetails)
                {
                    var response = await _bookService.GetBook(new Guid(item.SelecctedItemID));
                    if (response.Result)
                    {
                        var obj = new ShoppingCartDetailDto
                        {
                            BookID = response.Book.BookID,
                            Title = response.Book.Title,
                            PublicationDate = response.Book.PublicationDate
                        };

                        shoppingCartDetailDtos.Add(obj);
                    }
                }

                return new ShoppingCartDto
                {
                    ShoppingCartID = sessionCart.SessionCartID,
                    CreateDate = sessionCart.CreateAt,
                    shoppingCartDetails = shoppingCartDetailDtos
                };

            }
        }

    }
}
