using Astro.Database.Common;
using Microsoft.EntityFrameworkCore;
namespace Astro.WebApi.Common
{
    public class MigrateDbWorker<T> : IHostedService where T : DbContext // Класс MigrateDbWorker<T> реализует интерфейс IhostedService из либы Microsoft.Extensions.Hosting. Этот класс используется для миграции БД.
    {                                                                    // Класс MigrateDbWorker<T> имеет один обобщенный параметр T, который представляет тип контекста базы данных. Он требует, чтобы тип T наследовался от класса DbContext.
        private readonly IServiceProvider _provider;

        public MigrateDbWorker(IServiceProvider provider) // Класс MigrateDbWorker<T> имеет конструктор, который принимает IServiceProvider в качестве параметра. IServiceProvider используется для создания области действия и получения экземпляра класса DbMigrator<T>, который используется для выполнения миграции базы данных.
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken) // Метод StartAsync запускается при запуске сервиса, который реализует интерфейс IHostedService. Внутри этого метода создается область действия, и из нее получается экземпляр класса DbMigrator<T>. Затем вызывается метод Migrate, который применяет все ожидающие миграции базы данных для контекста T.
        {
            using var scope = _provider.CreateScope();
            var dbMigrator = scope.ServiceProvider.GetRequiredService<DbMigrator<T>>();
            await dbMigrator.Migrate(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask; // Метод StopAsync останавливает сервис, но в данном случае он ничего не делает и просто возвращает Task.CompletedTask.
    }
}
