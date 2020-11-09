using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MicroserviceBooks.Models;
using MicroserviceBooks.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBooks.Application
{
    public class QueryFilter
    {
        public class BookRequest : IRequest<BookDto>
        {
            public Guid BookID { get; set; }
        }

        public class Driver : IRequestHandler<BookRequest, BookDto>
        {
            private readonly DbContextBook _context;
            private readonly IMapper _mapper;

            public Driver(DbContextBook Context,IMapper mapper)
            {
                _context = Context;
                _mapper = mapper;
            }

            public async Task<BookDto> Handle(BookRequest request, CancellationToken cancellationToken)
            {
                var book = await _context.Books.Where(x => x.BookID == request.BookID).FirstOrDefaultAsync();

                if(book == null)
                {
                    throw new Exception("Libro no encontrado");
                }

                return _mapper.Map<Book, BookDto>(book);
            }
        }
    }
}
