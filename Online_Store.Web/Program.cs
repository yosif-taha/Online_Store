
using Online_Store.Web.Exctensions;

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
