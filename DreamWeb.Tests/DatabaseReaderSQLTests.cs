using DreamWeb.DAL;
using DreamWeb.DAL.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DreamWeb.Tests
{
    public class DatabaseReaderSQLTests
    {
        private readonly DreamsContext _context;

        public DatabaseReaderSQLTests()
        {
            var optionBuilder = new DbContextOptionsBuilder<DreamsContext>()
                .UseInMemoryDatabase(databaseName: "testDatabase");
            _context = new DreamsContext(optionBuilder.Options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.UserAccounts.AddRange(new List<UserAccount>()
            {
                new UserAccount()
                {
                    Email = "email",
                    Id = 0,
                    UserName = "firstUser",
                    CreationTime = DateTime.Now
                },
                new UserAccount()
                {
                    Email = "email2",
                    Id = 1,
                    UserName = "secondUser",
                    CreationTime = DateTime.Now
                },
                new UserAccount()
                {
                    Email = "takenEmail",
                    Id = 2,
                    UserName = "thirdUser",
                    CreationTime = DateTime.Now
                },
                new UserAccount()
                {
                    Email = "email4",
                    Id = 3,
                    UserName = "takenUsername",
                    CreationTime = DateTime.Now
                },
                new UserAccount()
                {
                    Email = "email5",
                    Id = 5,
                    UserName = "fifthUser",
                    CreationTime = DateTime.Now
                }
            });

            _context.SaveChanges();

            _context.DreamPublications.AddRange(new List<Dream>()
            {
                new Dream()
                {
                    AuthorID = "firstUser",
                    CreationDate = DateTime.Now,
                    Content = "empty",
                    Hours = "4",
                    Id = "id2",
                    IsPublic = false,
                    Name = "1",
                    Topics = "topics",
                    UserAccount = _context.UserAccounts.First(p => p.UserName == "firstUser")
                }
            });

            _context.SaveChanges();
        }

        [Fact]
        public async void GetUserAccountByUsernameAsync_ReturnsUser()
        {
            string username = "fifthUser";
            DatabaseReaderSQL databaseReader = new(_context);

            var user = await databaseReader.GetUserAccountByUsernameAsync(username);
            var userFromDb = _context.UserAccounts.First(p => p.UserName == username);

            user.Dreams.Should().BeEquivalentTo(userFromDb.Dreams);
            user.UserName.Should().BeEquivalentTo(userFromDb.UserName);
            user.Email.Should().BeEquivalentTo(userFromDb.Email);
        }

        [Fact]
        public async void AddDreamAsync_AddsDream()
        {
            Dream dream = new()
            {
                AuthorID = "firstUser",
                CreationDate = DateTime.Now,
                Content = "empty",
                Hours = "4",
                Id = "id1",
                IsPublic = false,
                Name = "1",
                Topics = "topics"
            };
            string username = "firstUser";

            DatabaseReaderSQL databaseReader = new(_context);

            await databaseReader.AddDreamAsync(dream, username);

            _context.DreamPublications.Should().Contain(p => p.Name == "1");
        }

        [Fact]
        public async void DeleteDreamAsync_DeletesDream()
        {
            string username = "firstUser";
            string dreamId = "id2";

            DatabaseReaderSQL databaseReader = new(_context);
            await databaseReader.DeleteDreamAsync(username, dreamId);

            _context.DreamPublications.Should().NotContain(p => p.Id == dreamId);
        }

        [Fact]
        public async void IsEmailTaken_ReturnsTrue()
        {
            string email = "takenEmail";
            DatabaseReaderSQL databaseReader = new(_context);

            bool isEmailTaken = await databaseReader.IsEmailTaken(email);

            isEmailTaken.Should().BeTrue();
        }

        [Fact]
        public async void IsEmailTaken_ReturnsFalse()
        {
            string email = "untakenEmail";
            DatabaseReaderSQL databaseReader = new(_context);

            bool isEmailTaken = await databaseReader.IsEmailTaken(email);

            isEmailTaken.Should().BeFalse();
        }

        [Fact]
        public async void IsUsernameTaken_ReturnsTrue()
        {
            string username = "takenUsername";
            DatabaseReaderSQL databaseReader = new(_context);

            bool isUsernameTaken = await databaseReader.IsUsernameTaken(username);

            isUsernameTaken.Should().BeTrue();
        }

        [Fact]
        public async void IsUsernameTaken_ReturnsFalse()
        {
            string username = "freeUsername";
            DatabaseReaderSQL databaseReader = new(_context);

            bool isUsernameTaken = await databaseReader.IsUsernameTaken(username);

            isUsernameTaken.Should().BeFalse();
        }

    }
}
