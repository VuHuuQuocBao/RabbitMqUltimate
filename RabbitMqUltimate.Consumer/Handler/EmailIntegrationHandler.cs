using Newtonsoft.Json;
using RabbitMqUltimate.EventBus.Bus;
using RabbitMqUltimate.EventBus.Core.EventsImplementations;

namespace RabbitMqUltimate.Consumer.Handler
{
    public class EmailIntegrationHandler : IIntegrationEventHandler<EmailIntegrationEvent>
    {
        //private readonly ILogger<EmailIntegrationHandler> _logger;

        public EmailIntegrationHandler(/*ILogger<EmailIntegrationHandler> logger*/)
        {
            //_logger = logger;
        }

        public async Task Handle(EmailIntegrationEvent @event)
        {
            //_logger.LogInformation("Start EmailIntegrationHandler");
            try
            {
                Console.WriteLine(JsonConvert.SerializeObject(@event));
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "EmailIntegrationHandler error: ");
            }
            //_logger.LogInformation("End EmailIntegrationHandler");
        }
    }
}
