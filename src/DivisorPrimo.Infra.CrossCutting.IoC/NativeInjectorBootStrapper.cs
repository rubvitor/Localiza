using DivisorPrimo.Infra.Data.Context;
using DivisorPrimo.Application.Interfaces;
using DivisorPrimo.Application.Services;
using DivisorPrimo.Domain.Commands;
using DivisorPrimo.Domain.Core.Events;
using DivisorPrimo.Domain.Events;
using DivisorPrimo.Domain.Interfaces;
using DivisorPrimo.Domain.Models;
using DivisorPrimo.Infra.CrossCutting.Bus;
using DivisorPrimo.Infra.Data.Context;
using DivisorPrimo.Infra.Data.EventSourcing;
using DivisorPrimo.Infra.Data.Repository;
using DivisorPrimo.Infra.Data.Repository.EventSourcing;
using DivisorPrimo.Services.Business;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace DivisorPrimo.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<INumeroAppService, NumeroAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<NumeroRegisteredEvent>, NumeroEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<NumeroCommand, object>, NumeroCommandHandler>();

            // Infra - Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DivisorPrimoContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();

            services.AddScoped<INumeroBusiness, NumeroBusiness>();
        }
    }
}