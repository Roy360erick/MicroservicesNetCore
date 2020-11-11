using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbirtMQ.Bus.Commands;
using RabbirtMQ.Bus.Events;
using RabbirtMQ.Bus.RabbitBus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RabbirtMQ.Bus.Implements
{
    public class RabbitEventBuss : IRabbitEventBuss
    {
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Dictionary<string, List<Type>> _drivers;
        private readonly List<Type> _eventTypes;

        public RabbitEventBuss(IMediator mediator,IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _drivers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public void Publish<T>(T @event) where T : Event
        {
            //Publish in buss 

            var factory = new ConnectionFactory
            {
                HostName = "rabbitweb",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", eventName, null, body);
            }
        }

        public Task SendCommand<T>(T Command) where T : Command
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventDrive<T>
        {
            var eventName = typeof(T).Name;
            var driverName = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_drivers.ContainsKey(eventName))
            {
                _drivers.Add(eventName, new List<Type>());
            }

            if(_drivers[eventName].Any(x => x.GetType() == driverName))
            {
                throw new ArgumentException("Duplicate type error");
            }

            _drivers[eventName].Add(driverName);

            var factory = new ConnectionFactory
            {
                HostName = "rabbitweb",
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var chanel = connection.CreateModel();

            chanel.QueueDeclare(eventName, false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(chanel);

            consumer.Received += Consumer_Delegate;

            chanel.BasicConsume(eventName, true, consumer);


        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                if (_drivers.ContainsKey(eventName))
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var subscriptions = _drivers[eventName];

                        foreach (var sb in subscriptions)
                        {
                            var driver = scope.ServiceProvider.GetService(sb); //Activator.CreateInstance(item);
                            if (driver == null) continue;

                            var eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);

                            var eventDS = JsonConvert.DeserializeObject(message, eventType);

                            var typeConcret = typeof(IEventDrive<>).MakeGenericType(eventType);

                            await (Task)typeConcret.GetMethod("Handle").Invoke(driver, new object[] { eventDS });
                        }
                    }
                        
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
