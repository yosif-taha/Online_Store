
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Store.Domain.Contracts;
using Online_Store.Persistence;
using Online_Store.Persistence.Data.Contexts;
using Online_Store.Services;
using Online_Store.Services.Abstractions;
using Online_Store.Services.Mapping.Products;
using Online_Store.Shared.ErrorModels;
using Online_Store.Web.Exctensions;
using Online_Store.Web.Middlewares;
using System.Threading.Tasks;

namespace Online_Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();

           await  app.ConfigureMaddelwares();

            app.Run();
        }
    }
}
