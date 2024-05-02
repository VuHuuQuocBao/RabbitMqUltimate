using RabbitMqUltimate.EventBus.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqUltimate.EventBus.Core.EventsImplementations
{
    public class EmailIntegrationEvent : IntegrationEvent
    {
        public string Subject { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public string Body { get; set; }
    }
}
