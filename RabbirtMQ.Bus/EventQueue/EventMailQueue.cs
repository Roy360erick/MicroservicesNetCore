using RabbirtMQ.Bus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbirtMQ.Bus.EventQueue
{
    public class EventMailQueue : Event
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public EventMailQueue(string to, string title, string content)
        {
            To = to;
            Title = title;
            Content = content;
        }


    }
}
