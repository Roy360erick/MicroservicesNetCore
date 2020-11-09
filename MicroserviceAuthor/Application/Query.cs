using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroserviceAuthor.Models;
using MicroserviceAuthor.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceAuthor.Application
{
    public class Query
    {
        public class RequestAuthor : IRequest<List<AuthorDto>>
        {
        }


        public class Driver : IRequestHandler<RequestAuthor, List<AuthorDto>>
        {
            private readonly DbContextAuthor _context;
            private readonly IMapper _mapper;

            public Driver(DbContextAuthor context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<AuthorDto>> Handle(RequestAuthor request, CancellationToken cancellationToken)
            {
                var authors = await _context.Authors.ToListAsync();
                return _mapper.Map<List<Author>, List<AuthorDto>>(authors);

            }
        }

    }
}
