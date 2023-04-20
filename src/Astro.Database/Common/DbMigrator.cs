using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Astro.Database.Common
{
    public class DbMigrator<TContext> where TContext : DbContext
    {
        private readonly ILogger<DbMigrator<TContext>> _logger;
        private readonly TContext _context;

        public DbMigrator(ILogger<DbMigrator<TContext>> logger, TContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Migrate(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing db migration...");
            try
            {
                await _context.Database.MigrateAsync(cancellationToken);
                _logger.LogInformation("Db migration successfully executed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Db migration completed with error.");
            }
        }
    }
}
// Этот код должен быть написан вручную разработчиком и находиться в проекте Database в папке Common в классе DbMigrator.
// Он представляет собой класс, который обеспечивает выполнение миграций базы данных, используя Entity Framework.
// Класс DbMigrator имеет конструктор, который принимает два параметра: ILogger и TContext, и метод Migrate, который выполняет миграцию базы данных.
// Логика класса DbMigrator зависит от требований приложения, но обычно этот класс обеспечивает автоматическое выполнение миграций при старте приложения
// или при изменении модели данных.
