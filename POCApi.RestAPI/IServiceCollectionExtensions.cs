using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POCApi.Core.DomainServices;
using POCApi.Core.Interfaces;
using POCApi.Core.Interfaces.IRepositories;
using POCApi.Infrastructure;
using POCApi.Infrastructure.Contexts;
using POCApi.Infrastructure.Repositories;
using System;
using FluentValidation;
using POCApi.Core.Entities;
using POCApi.RestAPI.Requests;
using POCApi.RestAPI.Validators;

namespace POCApi.RestAPI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<ICompartmentPickRepository, CompartmentPickRepository>();
            

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                // Infrastructure services

                // Domain services
                .AddScoped<CompartmentPickService>()
                // Validators
                .AddTransient<IValidator<ExamineCompartmentPickingRequest>, CompartmentPickValidator>();
        }
    }
}
