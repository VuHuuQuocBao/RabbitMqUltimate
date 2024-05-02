using RabbitMqUltimate.EventBus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqUltimate.EventBus.Core.Events
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;

        void AddSubscription(string eventName, Type typeEvent, Type typeHandler);

        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();
    }
    public class SubscriptionInfo
    {
        public bool IsDynamic { get; private set; }
        public Type HandlerType { get; private set; }

        private SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Subscription(bool isDynamic, Type handlerType) => new SubscriptionInfo(isDynamic, handlerType);
    }

    public class SubscriptionModel
    {
        public string EventName { get; set; }
        public Type TypeEvent { get; set; }
        public Type TypeHandler { get; set; }
    }
}
