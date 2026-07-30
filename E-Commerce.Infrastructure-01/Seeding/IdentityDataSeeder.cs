using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure_01.Identity.Data;
using E_Commerce.Infrastructure_01.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Infrastructure_01.Seeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContexts _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(
            StoreIdentityDbContexts dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataSeeder> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);

                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync(ct);

                if (!await _roleManager.Roles.AnyAsync(ct))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!await _userManager.Users.AnyAsync(ct))
                {
                    var admin = new ApplicationUser
                    {
                        DisplayName = "Mohammed Ahmed",
                        Email = "Mohammed@Gmail.com",
                        UserName = "Mohamed",
                        PhoneNumber = "01225770196"
                    };

                    var createResult = await _userManager.CreateAsync(admin, "P@ssw0rd");

                    if (createResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "Admin");
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Could Not Seed Default Admin User: {Errors}",
                            string.Join(";", createResult.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Identity Data Seeding Failed");
            }
        }
    }
}