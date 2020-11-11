using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MicroserviceBooks.Models;
using MicroserviceBooks.Persistence;
using RabbirtMQ.Bus.EventQueue;
using RabbirtMQ.Bus.RabbitBus;

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
            private readonly IRabbitEventBuss _rabbitEventBuss;

            public Driver(DbContextBook context, IRabbitEventBuss rabbitEventBuss)
            {
                _context = context;
                _rabbitEventBuss = rabbitEventBuss;
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

                _rabbitEventBuss.Publish(new EventMailQueue("", "Mail de prueba", ""));
                if (result > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo completar el registro del libro");
            }
        }
    }
}
