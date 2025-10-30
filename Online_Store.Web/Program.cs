
using Microsoft.EntityFrameworkCore;
using Online_Store.Domain.Contracts;
using Online_Store.Persistence;
using Online_Store.Persistence.Data.Contexts;
using Online_Store.Services;
using Online_Store.Services.Abstractions;
using Online_Store.Services.Mapping.Products;
using System.Threading.Tasks;

namespace Online_Store.Web
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OnlineStoreDbContext>( options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddScoped<IDbInitializer,DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IServicesManager,ServicesManager>();
            builder.Services.AddAutoMapper(M => M.AddProfile( new ProductProfile()));

            var app = builder.Build();

            #region Initialize DB

            using var scope = app.Services.CreateScope();

            var dbinitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Ask CLR to crate object from IDbInitializer
            await dbinitializer.IntiliazeAsync();
            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
