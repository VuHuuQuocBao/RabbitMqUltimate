using Autofac;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMqUltimate.Consumer1.DependencyInjection.Options;
using RabbitMqUltimate.Consumer1.Handler;
using RabbitMqUltimate.EventBus.Core.Events;
using RabbitMqUltimate.EventBus.Core.EventsImplementation;
using RabbitMqUltimate.EventBus.Core.EventsImplementations;
using RabbitMqUltimate.EventBus.RabbitMq;

namespace RabbitMqUltimate.Consumer.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                RabbitMQConfiguration rabitMQConfiguration = new();
                configuration.GetSection("RabitMQConfiguration").Bind(rabitMQConfiguration);
                var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = rabitMQConfiguration.HostName,
                };

                if (!string.IsNullOrEmpty(rabitMQConfiguration.UserName))
                    factory.UserName = rabitMQConfiguration.UserName;

                if (!string.IsNullOrEmpty(rabitMQConfiguration.Password))
                    factory.Password = rabitMQConfiguration.Password;

                if (!string.IsNullOrEmpty(rabitMQConfiguration.VirtualHost))
                    factory.VirtualHost = rabitMQConfiguration.VirtualHost;

                if (rabitMQConfiguration.Port.HasValue)
                    factory.Port = rabitMQConfiguration.Port.Value;

                return new RabbitMQPersistentConnection(factory, logger, rabitMQConfiguration.RetryCount);
            });
            RabbitMQConfiguration rabitMQConfiguratio1n = new();
            configuration.GetSection("RabitMQConfiguration").Bind(rabitMQConfiguratio1n);

            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

            services.AddSingleton<IEventBusRabbitMQ, EventBusRabbitMQ>(sp =>
            {
                var x = new ContainerBuilder();
                x.RegisterType<CustomHandler>().AsSelf();
                var y = x.Build();
                
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                //var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                RabbitMQConfiguration rabitMQConfiguration = new();
                configuration.GetSection("RabitMQConfiguration").Bind(rabitMQConfiguration);

                return new EventBusRabbitMQ(rabbitMQPersistentConnection,
                    logger, y.BeginLifetimeScope(), eventBusSubcriptionsManager,
                    queueName: rabitMQConfiguration.QueueName,
                    exchangetype: rabitMQConfiguration.Exchangetype,
                    exchangeName: rabitMQConfiguration.ExchangeName,
                    retryCount: rabitMQConfiguration.RetryCount);
            });

            return services;
        }

        public static IServiceCollection SubscribeEmailIntegrationEventEventToHandler(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();

            var eventBus = serviceProvider.GetRequiredService<IEventBusRabbitMQ>();
            eventBus.Subscribe<EmailIntegrationEvent, CustomHandler>();


            /*            using (var serviceProvider = services.BuildServiceProvider())
                        {
                            var eventBus = serviceProvider.GetRequiredService<IEventBusRabbitMQ>();
                            eventBus.Subscribe<EmailIntegrationEvent, EmailIntegrationHandler>();
                        }*/

            return services;
        }
    }
}
