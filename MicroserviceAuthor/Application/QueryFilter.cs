using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroserviceAuthor.Models;
using MicroserviceAuthor.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceAuthor.Application
{
    public class QueryFilter
    {
        public class RequestFilterAuthor : IRequest<AuthorDto>
        {
            public string AuthorGuid { get; set; }
        }

        public class Driver : IRequestHandler<RequestFilterAuthor, AuthorDto>
        {
            private readonly DbContextAuthor _context;
            private readonly IMapper _mapper;

            public Driver(DbContextAuthor context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<AuthorDto> Handle(RequestFilterAuthor request, CancellationToken cancellationToken)
            {
                var author = await _context.Authors.Where(author => author.AuthorGuid == request.AuthorGuid).FirstOrDefaultAsync();

                var authorDto = _mapper.Map<Author, AuthorDto>(author);

                if(author == null)
                {
                    throw new Exception("No se encontro el autor");
                }
                return authorDto;
            }
        }
    }
}
