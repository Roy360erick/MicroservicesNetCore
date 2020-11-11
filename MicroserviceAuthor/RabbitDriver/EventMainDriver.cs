using MessengerService.Mail.SendGrid.Models;
using MessengerService.Mail.SendGrid.Services;
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
        private readonly ISendGridService _sendGridService;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public EventMainDriver(ILogger<EventMainDriver> logger,ISendGridService sendGridService, 
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _logger = logger;
            _sendGridService = sendGridService;
            _configuration = configuration;
        }

        public async Task Handle(EventMailQueue @event)
        {
            _logger.LogInformation($"RabbitMQ{@event.Title}");
            SendGridData data = new SendGridData
            {
                Title = @event.Title,
                To = @event.To,
                Content = @event.Content,
                ToName = @event.To,
                SendGridAPIKey = _configuration["SendGrid:ApiKey"]
            };

            var result = await _sendGridService.SendMail(data);

            if (result.IsSuccess)
            {
                await Task.CompletedTask;
                return;
            }
        }
    }
}
