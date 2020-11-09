using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MicroserviceAuthor.Models;
using MicroserviceAuthor.Persistence;

namespace MicroserviceAuthor.Application
{
    public class New
    {
        public class Execute : IRequest
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public DateTime? Birthdate { get; set; }
        }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }

        public class Driver : IRequestHandler<Execute>
        {
            private readonly DbContextAuthor _context;

            public Driver(DbContextAuthor context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                Author author = new Author
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Birthdate = request.Birthdate,
                    AuthorGuid = Guid.NewGuid().ToString()
                };

                await _context.Authors.AddAsync(author);
                var result = await _context.SaveChangesAsync();

                if(result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo inserta el autor del libro");
                

            }
        }
    }
}
