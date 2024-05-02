using RabbitMqUltimate.EventBus.Bus;
using RabbitMqUltimate.EventBus.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqUltimate.EventBus.RabbitMq
{
    public interface IEventBusRabbitMQ
    {
        void Publish(IntegrationEvent rabbitMqEvent);

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
