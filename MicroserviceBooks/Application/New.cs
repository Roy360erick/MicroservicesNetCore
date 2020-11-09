using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MicroserviceBooks.Models;
using MicroserviceBooks.Persistence;

namespace MicroserviceBooks.Application
{
    public class New
    {
        public class RequestBook : IRequest
        {
            public string Title { get; set; }
            public DateTime PublicationDate { get; set; }
            public Guid AuthorGuid { get; set;}
        }

        public class Validator : AbstractValidator<RequestBook>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
                RuleFor(x => x.AuthorGuid).NotEmpty();
            }
        }

        public class Driver : IRequestHandler<RequestBook>
        {
            private readonly DbContextBook _context;

            public Driver(DbContextBook context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(RequestBook request, CancellationToken cancellationToken)
            {
                Book book = new Book
                {
                    Title = request.Title,
                    PublicationDate = request.PublicationDate,
                    AuthorGuid = request.AuthorGuid
                };
                await _context.Books.AddAsync(book);
                var result = await _context.SaveChangesAsync();

                if(result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo completar el registro del libro");
            }
        }
    }
}
