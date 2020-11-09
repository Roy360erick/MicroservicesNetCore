using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroserviceShoppingCart.Models;
using MicroserviceShoppingCart.Persistence;

namespace MicroserviceShoppingCart.Application
{
    public class New
    {
        public class SessionCartRequest : IRequest
        {
            public DateTime? CreateAt { get; set; }
            public List<string> SelecctedItems { get; set; }
        }

        public class Driver : IRequestHandler<SessionCartRequest>
        {
            private readonly DbContextCart _context;

            public Driver(DbContextCart context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(SessionCartRequest request, CancellationToken cancellationToken)
            {
                SessionCart sessionCart = new SessionCart
                {
                    CreateAt = request.CreateAt
                };

                await _context.SessionCarts.AddAsync(sessionCart);
                var result = await _context.SaveChangesAsync();

                if(result == 0)
                {
                    throw new Exception("Error en la creacion de la session");
                }


                foreach (var item in request.SelecctedItems)
                {
                    SessionCartDetail sessionCartDetail = new SessionCartDetail
                    {
                        CreateAt = DateTime.Now,
                        SessionCartID = sessionCart.SessionCartID,
                        SelecctedItemID = item
                    };

                    await _context.SessionCartDetails.AddAsync(sessionCartDetail);
                }

                var result1 = await _context.SaveChangesAsync();

                if(result1 > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo agregar los produtos a la session");

            }
        }
    }
}
