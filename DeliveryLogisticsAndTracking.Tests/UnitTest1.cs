using DeliveryLogisticsAndTracking.Data;
using DeliveryLogisticsAndTracking.Models;
using DeliveryLogisticsAndTracking.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryLogisticsAndTracking.Tests
{
    public static class TestHelpers
    {
        public static AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }

    [TestFixture]
    public class UserServiceTests
    {
        private AppDbContext _context = null!;
        private UserService _service = null!;

        [SetUp]
        public void Setup()
        {
            _context = TestHelpers.GetInMemoryDbContext();
            _service = new UserService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        // --- Existing CRUD tests ---
        [Test]
        public async Task AddUserAsync_ShouldAddUser()
        {
            var user = new User
            {
                Name = "Test User",
                Email = "test@example.com",
                UserType = "Admin",
                Phone = "12345678",
                DOB = new DateTime(1990, 1, 1)
            };

            await _service.AddUserAsync(user);
            var users = await _service.GetUsersAsync();

            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("Test User", users.First().Name);
        }

        [Test]
        public async Task ValidateLoginAsync_ShouldReturnTrueForCorrectPassword()
        {
            var user = new User
            {
                Name = "Login Test",
                Email = "login@test.com",
                UserType = "Admin",
                Phone = "12345678",
                DOB = new DateTime(1990, 1, 1)
            };

            await _service.AddUserAsync(user);
            var password = "secret";
            var hash = _service.HashPassword(password);
            await _service.AddUserPasswordAsync(user.UserId, hash);

            var result = await _service.ValidateLoginAsync(user.UserId, password);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ValidateLoginAsync_ShouldReturnFalseForWrongPassword()
        {
            var user = new User
            {
                Name = "Login Test",
                Email = "login@test.com",
                UserType = "Admin",
                Phone = "12345678",
                DOB = new DateTime(1990, 1, 1)
            };

            await _service.AddUserAsync(user);
            var password = "secret";
            var hash = _service.HashPassword(password);
            await _service.AddUserPasswordAsync(user.UserId, hash);

            var result = await _service.ValidateLoginAsync(user.UserId, "wrongpassword");
            Assert.IsFalse(result);
        }

        // --- New Validation Tests ---

        [Test]
        public void AddUserAsync_InvalidEmail_ShouldThrowException()
        {
            var user = new User
            {
                Name = "Invalid Email",
                Email = "not-an-email",
                UserType = "Admin",
                Phone = "12345678",
                DOB = new DateTime(1990, 1, 1)
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await _service.AddUserAsync(user));
        }

        [Test]
        public void AddUserAsync_InvalidPhone_ShouldThrowException()
        {
            var user = new User
            {
                Name = "Invalid Phone",
                Email = "valid@example.com",
                UserType = "Admin",
                Phone = "abcd123",
                DOB = new DateTime(1990, 1, 1)
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await _service.AddUserAsync(user));
        }

        [Test]
        public void AddUserAsync_InvalidDOB_ShouldThrowException()
        {
            var user = new User
            {
                Name = "Invalid DOB",
                Email = "valid@example.com",
                UserType = "Admin",
                Phone = "12345678",
                DOB = DateTime.Now.AddYears(1) // DOB in the future
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await _service.AddUserAsync(user));
        }

        [Test]
        public void AddUserAsync_ShouldThrowForEmptyName()
        {
            var user = new User
            {
                Name = "",
                Email = "test@example.com",
                UserType = "Admin",
                Phone = "12345678",
                DOB = new DateTime(1990, 1, 1)
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await _service.AddUserAsync(user));
        }
    }
}