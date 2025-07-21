using Domain.Contracts;
using System.Text.Json;

namespace Presistance.Data.DataSeeding
{
    public class DbIntializer : IDbIntializer
    {
        private readonly AppDbContext _dbContext;

        public DbIntializer(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task IntializeAsync()
        {
            try
            {
                if(_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();
                    if(!_dbContext.ProductTypes.Any())
                    {
                        //C:\Users\DELL\Desktop\Route.net\C#\E-Commerce\Infrastructure\Presistance\Data\DataSeeding\types.json
                        var typeData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\DataSeeding\types.json");
                        //Conert from json to c# object [desirlization]
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                        if(types is not null &&  types.Any() )
                        {
                            await _dbContext.AddRangeAsync(types);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    if (!_dbContext.ProductBrands.Any())
                    {
                        //C:\Users\DELL\Desktop\Route.net\C#\E-Commerce\Infrastructure\Presistance\Data\DataSeeding\brands.json
                        var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\DataSeeding\brands.json");
                        //Conert from json to c# object [desirlization]
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                        if (brands is not null && brands.Any())
                        {
                            await _dbContext.AddRangeAsync(brands);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    if (!_dbContext.Products.Any())
                    {
                        //C:\Users\DELL\Desktop\Route.net\C#\E-Commerce\Infrastructure\Presistance\Data\DataSeeding\products.json
                        var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Data\DataSeeding\products.json");
                        //Conert from json to c# object [desirlization]
                        var products = JsonSerializer.Deserialize<List<Product>>(productData);
                        if (products is not null && products.Any())
                        {
                            await _dbContext.AddRangeAsync(products);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
