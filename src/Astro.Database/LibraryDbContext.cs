using Astro.Domain.Entities; // подключение пространства имен, где определены классы сущностей, связанных с базой данных.
using Microsoft.EntityFrameworkCore; // подключение пространства имен, где определены классы фреймворка Entity Framework Core для работы с базами данных.
using Microsoft.EntityFrameworkCore.Design; //  подключение пространства имен, где определен интерфейс IDesignTimeDbContextFactory, необходимый для работы с миграциями. 
using Microsoft.Extensions.Configuration; // подключение пространства имен, где определен интерфейс IConfiguration, используемый для получения конфигурационных данных из файлов или других источников.

namespace Astro.Database // объявление пространства имен, где определен класс LibraryDbContext.
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class LibraryDbContext : DbContext // определение класса LibraryDbContext, который наследуется от класса DbContext.
    {
        public DbSet<User> User; // определение свойства User типа DbSet<User>, которое представляет собой таблицу пользователей в базе данных.
        public DbSet<Book> Book; // определение свойства Book типа DbSet<Book>, которое представляет собой таблицу книг в базе данных.

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">опции контекста</param>
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) // конструктор класса LibraryDbContext, который принимает параметр options типа DbContextOptions<LibraryDbContext>. Он вызывает конструктор базового класса DbContext с параметром options.
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // установка параметра конфигурации, указывающего, что необходимо использовать устаревшее поведение временных меток при работе с PostgreSQL.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // переопределенный метод, который вызывается в момент настройки контекста базы данных.
        {
            optionsBuilder.UseNpgsql("server=localhost;Port=5432;Database=AstroLibrary;User ID=postgres;Password=27503"); // определение строки подключения к базе данных PostgreSQL. Она содержит информацию о сервере, порте, базе данных, имени пользователя и пароле.
            optionsBuilder.UseSnakeCaseNamingConvention(); //  установка конвенции именования столбцов в базе данных в формате "snake_case".
            base.OnConfiguring(optionsBuilder); // вызов метода базового класса для настройки контекста.
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder) // переопределенный метод, который вызывается в момент создания модели данных.
        {
            base.OnModelCreating(modelBuilder); // вызов метода базового класса для настройки модели.

            modelBuilder.Entity<User>().ToTable("user"); //  определение таблицы user для сущности User.
            modelBuilder.Entity<Book>().ToTable("book"); //  определение таблицы book для сущности Book.
        }
    }
}
// Класс LibraryDbContext отвечает за создание контекста базы данных и определение схемы базы данных через использование Entity Framework Core.
// Он содержит определения для всех таблиц базы данных, которые необходимо создать и использовать в приложении, определения связей между таблицами
// и определяет параметры подключения к базе данных, включая строку подключения, которая указывает на расположение и учетные данные для доступа к базе данных.
// Также данный класс содержит методы, которые позволяют определить способ создания и обновления базы данных при первом запуске приложения, а также в процессе разработки и изменения приложения.