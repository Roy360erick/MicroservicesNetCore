using System;
using System.Threading.Tasks;
using MediatR;
using MicroserviceShoppingCart.Application;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ShoppingCartDto> GetById(int id)
        {
            return await _mediator.Send(new Query.RequestShoppingCart { ShoppingCartId = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.SessionCartRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
