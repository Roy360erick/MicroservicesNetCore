using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroserviceBooks.Models;
using MicroserviceBooks.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBooks.Application
{
    public class Query 
    {
        public class BookRequest : IRequest<List<BookDto>> { }

        public class Driver : IRequestHandler<BookRequest, List<BookDto>>
        {
            private readonly DbContextBook _context;
            private readonly IMapper _mapper;

            public Driver(DbContextBook context, IMapper mapper) 
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<BookDto>> Handle(BookRequest request, CancellationToken cancellationToken)
            {
                var books = await _context.Books.ToListAsync();
                return _mapper.Map<List<Book>, List<BookDto>>(books);
            }
        }
    }
}
