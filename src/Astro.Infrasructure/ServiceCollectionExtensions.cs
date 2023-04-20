using Astro.Infrasructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Astro.Infrasructure
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
        public static IServiceCollection AddInfrasructure(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
