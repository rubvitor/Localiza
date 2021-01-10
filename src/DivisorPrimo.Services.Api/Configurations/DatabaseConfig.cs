using System;
using System.Threading.Tasks;
using DivisorPrimo.Infra.Data.Context;
using DivisorPrimo.Domain.Interfaces;
using DivisorPrimo.Domain.Models;
using DivisorPrimo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DivisorPrimo.Services.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<EventStoreSqlContext>(options =>
                options.UseInMemoryDatabase(databaseName: configuration.GetValue<string>("DatabaseName")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDbContext<DivisorPrimoContext>(options =>
                options.UseInMemoryDatabase(databaseName: configuration.GetValue<string>("DatabaseName")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }

    public class ELPIdentityInitializer
    {
        private readonly IUserRepository _userRepository;

        public ELPIdentityInitializer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Seed()
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "manager",
                PasswordHash = "123456",
                Name = "Numero Manager",
                Email = "manager@numero.com"
            };

            _userRepository.Add(user);

            await _userRepository.UnitOfWork.Commit();
        }
    }
}