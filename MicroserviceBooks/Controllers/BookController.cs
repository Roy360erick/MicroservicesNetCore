using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MicroserviceBooks.Application;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceBooks.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<List<BookDto>> Get()
        {
            return await _mediator.Send(new Query.BookRequest());
        }

        [HttpGet]
        [Route("GetById/{Id}")]
        public async Task<BookDto> GetById(Guid Id)
        {
            return await _mediator.Send(new QueryFilter.BookRequest { BookID = Id});
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Unit>> Create(New.RequestBook request)
        {
            return await _mediator.Send(request);
        }
    }
}
