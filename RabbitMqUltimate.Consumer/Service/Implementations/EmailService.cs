
using RabbitMqUltimate.Consumer.Service.Abstractions;
using RabbitMqUltimate.EventBus.Core.Events;
using RabbitMqUltimate.EventBus.Core.EventsImplementations;
using RabbitMqUltimate.EventBus.RabbitMq;

namespace RabbitMqUltimate.Consumer.Service.Implementations
{
    public class EmailService : IEmailService
    {
        //private readonly TemplateViewProvider _templateViewProvider;
        private readonly IEventBusRabbitMQ _eventBusRabbitMQ;

        public EmailService(/*TemplateViewProvider templateViewProvider,
            IEventBusRabbitMQ eventBusRabbitMQ*/
            IEventBusRabbitMQ eventBusRabbitMQ
            )
        {
            /*_templateViewProvider = templateViewProvider;
            _eventBusRabbitMQ = eventBusRabbitMQ;*/
            _eventBusRabbitMQ = eventBusRabbitMQ;
        }

        public async Task SendEmailAsync()
        {
            var emailEvent = new EmailIntegrationEvent
            {
                To = new List<string> { "zzz@gmail.com" },
                Subject = "Test",
                Body = "Test"
            };

            await Task.Run(() => _eventBusRabbitMQ.Publish(emailEvent));
        }
    }
}
