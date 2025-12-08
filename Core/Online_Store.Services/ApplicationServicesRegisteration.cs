using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Online_Store.Services.Abstractions;
using Online_Store.Services.Mapping.Baskets;
using Online_Store.Services.Mapping.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IServicesManager, ServicesManager>();
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            return services;
        }
    }
}
