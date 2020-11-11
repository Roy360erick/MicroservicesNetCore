using RabbirtMQ.Bus.Commands;
using RabbirtMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbirtMQ.Bus.RabbitBus
{
    public interface IRabbitEventBuss
    {
        Task SendCommand<T>( T Command) where T: Command;
        void Publish<T>(T @event) where T:Event;
        void Subscribe<T, TH>() where T : Event where TH : IEventDrive<T>; 
    }
}
