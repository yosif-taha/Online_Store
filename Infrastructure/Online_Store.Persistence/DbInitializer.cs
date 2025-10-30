using Microsoft.EntityFrameworkCore;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites.Products;
using Online_Store.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Online_Store.Persistence
{                              // Use primary ctor to ask clr create object
    public class DbInitializer(OnlineStoreDbContext _context) : IDbInitializer
    {
       
        public async Task IntiliazeAsync()
        {
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) // GetPendingMigrationsAsync():- this fun to get all migration not appling to database. Any():- return true or false. 
            {
                await _context.Database.MigrateAsync();        //MigrateAsync() :- for appling migration in DB.
            }

            //Data Sedding :- copy data from json file to tables in DB

            if (!_context.ProductBrands.Any())
            {
                // 1. Data Sedding for product brand

                //1.1. read all data from json file 'brand.json'
                // C:\Users\ywsfw\source\repos\Online_Store\Infrastructure\Online_Store.Persistence\Data\Data Seeding\brands.json
                var brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Online_Store.Persistence\Data\Data Seeding\brands.json"); // @ :- to delete use double // , 

                // 1.2. convert jsonstring to list<ProductPrand>
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata); // this fun convert brandsdata to type from (List<ProductBrand)
                
                 //1.3.Add list to DB
                if (brands is not null && brands.Count > 0)  // Chek to Deserialize succes and there is brands in this file
                {
                    await _context.ProductBrands.AddRangeAsync(brands); //if true added this in productbrands table 
                }
            }

            // 2. Data Sedding for product type

            if (!_context.ProductTypes.Any())
            {
                // 1. Data Sedding for product type

                //1.1. read all data from json file 'types.json'
                // C:\Users\ywsfw\source\repos\Online_Store\Infrastructure\Online_Store.Persistence\Data\Data Seeding\Types.json
                var Typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\Online_Store.Persistence\Data\Data Seeding\Types.json"); // @ :- to delete use double // , 

                // 1.2. convert jsonstring to list<ProductPrand>
                var types = JsonSerializer.Deserialize<List<ProductType>>(Typesdata); // this fun convert brandsdata to type from (List<ProductTypes)

                //1.3.Add list to DB
                if (types is not null && types.Count > 0)  // Chek to Deserialize succes and there is types in this file
                {
                    await _context.ProductTypes.AddRangeAsync(types); //if true added this in producttypes table 
                }
            }

            // 3. Data Sedding for product

            if (!_context.Products.Any())
            {
                // 1. Data Sedding for product brand

                //1.1. read all data from json file 'brand.json'
                // C:\Users\ywsfw\source\repos\Online_Store\Infrastructure\Online_Store.Persistence\Data\Data Seeding\brands.json
                var productdata = await File.ReadAllTextAsync(@"..\Infrastructure\Online_Store.Persistence\Data\Data Seeding\products.json"); // @ :- to delete use double // , 

                // 1.2. convert jsonstring to list<ProductPrand>
                var products = JsonSerializer.Deserialize<List<Product>>(productdata); // this fun convert brandsdata to type from (List<ProductBrand)

                //1.3.Add list to DB
                if (products is not null && products.Count > 0)  // Chek to Deserialize succes and there is brands in this file
                {
                    await _context.Products.AddRangeAsync(products); //if true added this in productbrands table 
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
