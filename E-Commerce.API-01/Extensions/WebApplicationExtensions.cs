using E_Commerce.Domain.Contracts;

namespace E_Commerce.API_01.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var IdentitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");


            await seeder.SeedAsync();
            await IdentitySeeder.SeedAsync();

            return app;
        }
    }
}
