namespace BookApp.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Data.Models;
    using BookApp.Data;
    using Microsoft.EntityFrameworkCore;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email, string password)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            RoleManager<IdentityRole>? roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            IUserStore<ApplicationUser>? userStore = serviceProvider.GetService<IUserStore<ApplicationUser>>();
            UserManager<ApplicationUser>? userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager),
                    $"Service for {typeof(RoleManager<IdentityRole>)} cannot be obtained!");
            }

            if (userStore == null)
            {
                throw new ArgumentNullException(nameof(userStore),
                    $"Service for {typeof(IUserStore<ApplicationUser>)} cannot be obtained!");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager),
                    $"Service for {typeof(UserManager<ApplicationUser>)} cannot be obtained!");
            }

            Task.Run(async () =>
            {
                // Create Admin role if it does not exist
                bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
                IdentityRole? adminRole = null;
                if (!adminRoleExists)
                {
                    adminRole = new IdentityRole("Admin");
                    IdentityResult result = await roleManager.CreateAsync(adminRole);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while creating the {"Admin"} role!");
                    }
                }
                else
                {
                    adminRole = await roleManager.FindByNameAsync("Admin");
                }

                // Create User role if it does not exist
                bool userRoleExists = await roleManager.RoleExistsAsync("User");
                IdentityRole? userRole = null;
                if (!userRoleExists)
                {
                    userRole = new IdentityRole("User");
                    IdentityResult result = await roleManager.CreateAsync(userRole);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while creating the {"User"} role!");
                    }
                }
                else
                {
                    userRole = await roleManager.FindByNameAsync("User");
                }

                // Check if the admin user exists, if not create it
                ApplicationUser? adminUser = await userManager.FindByEmailAsync(email);
                if (adminUser == null)
                {
                    adminUser = await CreateAdminUserAsync(email, password, userStore, userManager);
                }

                // Check if the admin is already in the Admin role
                if (await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    return app;
                }

                // Add the admin user to the Admin role
                IdentityResult userResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Error occurred while adding the user {email} to the Admin role!");
                }

                // Create a normal user if you want and assign them to the User role
                ApplicationUser? normalUser = await userManager.FindByEmailAsync("user@example.com"); // Change the email for a normal user
                if (normalUser == null)
                {
                    normalUser = await CreateNormalUserAsync("user@example.com", "password123", userStore, userManager); // Use a password here
                }

                if (!await userManager.IsInRoleAsync(normalUser, "User"))
                {
                    IdentityResult userResult2 = await userManager.AddToRoleAsync(normalUser, "User");
                    if (!userResult2.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while adding the user {normalUser.Email} to the User role!");
                    }
                }

                return app;
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }

        private static async Task<ApplicationUser> CreateAdminUserAsync(string email, string password,
            IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            IdentityResult result = await userManager.CreateAsync(applicationUser, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error occurred while registering Admin user!");
            }

            return applicationUser;
        }

        private static async Task<ApplicationUser> CreateNormalUserAsync(string email, string password,
            IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            ApplicationUser normalUser = new ApplicationUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            IdentityResult result = await userManager.CreateAsync(normalUser, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error occurred while registering normal user!");
            }

            return normalUser;
        }
    }

}