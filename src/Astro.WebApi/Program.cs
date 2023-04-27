using Astro.Database;
using Astro.Application;
using Astro.WebApi.Common;
using Astro.WebApi.Services;

namespace Astro.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAuthentication().AddNegotiate();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddHostedService<MigrateDbWorker<LibraryDbContext>>();

            //////////////
            builder.Services.AddDbContext<LibraryDbContext>();
            builder.Services.AddTransient<UserService>();
            builder.Services.AddTransient<BookService>();
            builder.Services.AddTransient<ReviewService>(); // зарегистрировав ReviewService с использованием метода AddTransient решило проблему с внедрением зависимостей
            /////////////


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}