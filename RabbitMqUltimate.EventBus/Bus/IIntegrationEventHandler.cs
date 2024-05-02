using RabbitMqUltimate.EventBus.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqUltimate.EventBus.Bus
{
    public interface IIntegrationEventHandler<in T> where T : IntegrationEvent
    {
        Task Handle(T @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
