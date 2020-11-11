using Microsoft.Extensions.Logging;
using RabbirtMQ.Bus.EventQueue;
using RabbirtMQ.Bus.RabbitBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceAuthor.RabbitDriver
{
    public class EventMainDriver : IEventDrive<EventMailQueue>
    {
        private readonly ILogger<EventMainDriver> _logger;

        public EventMainDriver(ILogger<EventMainDriver> logger)
        {
            _logger = logger;
        }

        public Task Handle(EventMailQueue @event)
        {
            _logger.LogInformation($"RabbitMQ{@event.Title}");
            return Task.CompletedTask;
        }
    }
}
