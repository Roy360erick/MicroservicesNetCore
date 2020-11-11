using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerService.Mail.SendGrid.Models
{
    public class SendGridData
    {
        public string SendGridAPIKey { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
