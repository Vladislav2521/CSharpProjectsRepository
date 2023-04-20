using Astro.Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Astro.Database
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
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbMigrator<LibraryDbContext>>();
            return services;
        }
    }
}
