using Newtonsoft.Json;
using RabbitMqUltimate.EventBus.Bus;
using RabbitMqUltimate.EventBus.Core.EventsImplementations;

namespace RabbitMqUltimate.Consumer1.Handler
{
    public class CustomHandler : IIntegrationEventHandler<EmailIntegrationEvent>
    {
        //private readonly ILogger<EmailIntegrationHandler> _logger;

        public CustomHandler(/*ILogger<EmailIntegrationHandler> logger*/)
        {
            //_logger = logger;
        }

        public async Task Handle(EmailIntegrationEvent @event)
        {
            //_logger.LogInformation("Start EmailIntegrationHandler");
            try
            {
                Console.WriteLine("Second Consumer");
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
