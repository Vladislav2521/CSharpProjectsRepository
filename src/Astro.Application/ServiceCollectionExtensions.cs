using Astro.Infrasructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Astro.Application
{
    /// <summary>
    /// Класс внедрения зависимости данного слоя.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет зависимости в общий контейнер зависимостей.
        /// </summary>
        /// <param name="services">Контейнер зависимостей.</param>
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddInfrasructure();
            return services;
        }
    }
}
