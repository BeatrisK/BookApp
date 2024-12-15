using BookApp.Data.Models;
using BookApp.Services.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Data;

namespace BookApp.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService userService;
        private BookDbContext context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<BookDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            context = new BookDbContext(options);
            userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context),
                null, null, null, null, null, null, null, null);
            roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context),
                null, null, null, null);

            userService = new UserService(userManager, roleManager);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
            userManager.Dispose();
            roleManager.Dispose();
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers_WhenThereAreUsersInTheDb()
        {
            var user1 = new ApplicationUser { UserName = "user1@example.com", Email = "user1@example.com" };
            var user2 = new ApplicationUser { UserName = "user2@example.com", Email = "user2@example.com" };
            await userManager.CreateAsync(user1);
            await userManager.CreateAsync(user2);

            var users = await userService.GetAllUsersAsync();

            Assert.That(users.Count(), Is.EqualTo(2));
            Assert.That(users.ElementAt(0).Email, Is.EqualTo("user1@example.com"));
            Assert.That(users.ElementAt(1).Email, Is.EqualTo("user2@example.com"));
        }

        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnTrue_WhenUserExists()
        {
            var user = new ApplicationUser { UserName = "user@example.com", Email = "user@example.com" };
            await userManager.CreateAsync(user);

            var exists = await userService.UserExistsByIdAsync(user.Id.ToString());

            Assert.That(exists, Is.True);
        }

        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var exists = await userService.UserExistsByIdAsync("non-existing-id");

            Assert.That(exists, Is.False);
        }

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
        {
            var result = await userService.AssignUserToRoleAsync("testuser@example.com", "NonExistentRole");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var role = "Admin";
            await roleManager.CreateAsync(new IdentityRole(role));

            var result = await userService.AssignUserToRoleAsync("non-existing-id", role);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RemoveUserRoleAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
        {
            var user = new ApplicationUser { UserName = "user@example.com", Email = "user@example.com" };
            await userManager.CreateAsync(user);

            var result = await userService.RemoveUserRoleAsync(user.Id.ToString(), "non-existing-role");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task SoftDeleteUserAsync_ShouldReturnTrue_WhenUserIsSoftDeletedSuccessfully()
        {
            var user = new ApplicationUser { UserName = "user@example.com", Email = "user@example.com" };
            await userManager.CreateAsync(user);

            var result = await userService.SoftDeleteUserAsync(user.Id.ToString());

            Assert.That(result, Is.True);
            var deletedUser = await userManager.FindByIdAsync(user.Id.ToString());
            Assert.That(deletedUser.IsDeleted, Is.True);
        }

        [Test]
        public async Task SoftDeleteUserAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var result = await userService.SoftDeleteUserAsync("non-existing-id");

            Assert.That(result, Is.False);
        }
    }
}
