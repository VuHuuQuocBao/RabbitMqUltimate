using Autofac.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMqUltimate.Consumer.DependencyInjection.Options;
using RabbitMqUltimate.Consumer.Service.Abstractions;
using RabbitMqUltimate.Consumer.Service.Implementations;
using RabbitMqUltimate.Producer.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DI

// IOC Handler
//builder.Services.RabbitMQHandlerIOCConfigure(builder.Configuration);

// just DI RabbitMQ, when publish message, will start to connect to host
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddTransient<IEmailService, EmailService>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/public", (IEmailService _emailService) =>
{
    _emailService.SendEmailAsync();
    return new JsonResult("Ok");
});

app.UseHttpsRedirection();

app.Run();

