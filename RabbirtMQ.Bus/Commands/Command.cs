using RabbirtMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbirtMQ.Bus.Commands
{
    public class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
