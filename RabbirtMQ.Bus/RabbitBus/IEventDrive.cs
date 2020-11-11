using RabbirtMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbirtMQ.Bus.RabbitBus
{
    public interface IEventDrive<in TEvent> : IEventDrive where TEvent:Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventDrive
    {

    }
}
