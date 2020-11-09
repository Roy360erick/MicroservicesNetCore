using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MicroserviceAuthor.Application;
using MicroserviceAuthor.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceAuthor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<List<AuthorDto>> Get()
        {
            return await _mediator.Send(new Query.RequestAuthor());
        }

        [Route("GetById/{id}")]
        [HttpGet]
        public async Task<AuthorDto> GetById(string id)
        {
            return await _mediator.Send(new QueryFilter.RequestFilterAuthor{ AuthorGuid = id });
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Execute request)
        {
            return await _mediator.Send(request);
        }


    }
}
