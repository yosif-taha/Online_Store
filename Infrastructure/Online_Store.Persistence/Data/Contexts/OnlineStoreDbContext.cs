using Microsoft.EntityFrameworkCore;
using Online_Store.Domain.Entites.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Persistence.Data.Contexts
{
    public class OnlineStoreDbContext : DbContext
    {

        public OnlineStoreDbContext( DbContextOptions<OnlineStoreDbContext> options) : base(options)          
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
