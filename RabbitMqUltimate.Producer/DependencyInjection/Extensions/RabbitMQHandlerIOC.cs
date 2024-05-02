using RabbitMqUltimate.Consumer.Service.Abstractions;
using RabbitMqUltimate.Consumer.Service.Implementations;

namespace RabbitMqUltimate.Producer.DependencyInjection.Extensions
{
    public static class RabbitMQHandlerIOC
    {
        public static IServiceCollection RabbitMQHandlerIOCConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
