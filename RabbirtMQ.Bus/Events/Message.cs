using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbirtMQ.Bus.Events
{
    public class Message : IRequest<bool>
    {
        public string MessageType { get; protected set; }

        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
