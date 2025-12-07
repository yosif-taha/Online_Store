using Microsoft.AspNetCore.Mvc;
using Online_Store.Domain.Contracts;
using Online_Store.Persistence;
using Online_Store.Services;
using Online_Store.Shared.ErrorModels;
using Online_Store.Web.Middlewares;

namespace Online_Store.Web.Exctensions
{
    public static class Extensions
    {
        //this fonction for all services in program.cs
        public static IServiceCollection AddAllServices (this IServiceCollection services , IConfiguration configuration)
        {

            services.AddWebServices();
            services.ConfigureBehaviorOptions();
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);
            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection ConfigureBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any()).
                                                         Select(M => new ValidationError()
                                                         {
                                                             Field = M.Key,
                                                             Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                                         });
                    var reponse = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(reponse);
                };
            });
            return services;
        }



        //this fonction for all configuration after build in program.cs
        public static async Task<WebApplication> ConfigureMaddelwares(this WebApplication app)
        {
            #region Initialize DB
            await app.SeedingData();
            #endregion

            app.UseGlobalErrorHandling();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandleMaddleware>();
            return app;
        }

        private static async Task<WebApplication> SeedingData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();

            var dbinitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Ask CLR to crate object from IDbInitializer
            await dbinitializer.IntiliazeAsync();
            return app;
        }
    }
}
