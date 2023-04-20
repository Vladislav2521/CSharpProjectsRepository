using Astro.Database.Common;
using Microsoft.EntityFrameworkCore;
namespace Astro.WebApi.Common
{
    public class MigrateDbWorker<T> : IHostedService where T : DbContext // ����� MigrateDbWorker<T> ��������� ��������� IhostedService �� ���� Microsoft.Extensions.Hosting. ���� ����� ������������ ��� �������� ��.
    {                                                                    // ����� MigrateDbWorker<T> ����� ���� ���������� �������� T, ������� ������������ ��� ��������� ���� ������. �� �������, ����� ��� T ������������ �� ������ DbContext.
        private readonly IServiceProvider _provider;

        public MigrateDbWorker(IServiceProvider provider) // ����� MigrateDbWorker<T> ����� �����������, ������� ��������� IServiceProvider � �������� ���������. IServiceProvider ������������ ��� �������� ������� �������� � ��������� ���������� ������ DbMigrator<T>, ������� ������������ ��� ���������� �������� ���� ������.
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken) // ����� StartAsync ����������� ��� ������� �������, ������� ��������� ��������� IHostedService. ������ ����� ������ ��������� ������� ��������, � �� ��� ���������� ��������� ������ DbMigrator<T>. ����� ���������� ����� Migrate, ������� ��������� ��� ��������� �������� ���� ������ ��� ��������� T.
        {
            using var scope = _provider.CreateScope();
            var dbMigrator = scope.ServiceProvider.GetRequiredService<DbMigrator<T>>();
            await dbMigrator.Migrate(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask; // ����� StopAsync ������������� ������, �� � ������ ������ �� ������ �� ������ � ������ ���������� Task.CompletedTask.
    }
}
