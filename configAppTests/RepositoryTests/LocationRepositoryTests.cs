using config_app;
using config_app.DAL;
using config_app.Models;
using config_app.Repositories;
using config_app.Repositories.Abstractions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace configAppTests
{
    [TestClass]
    public class LocationRepositoryTests
    {
        private static SqliteConnection _connection;
        private static DbContextOptions<ConfigAppContext> _options;
        [TestInitialize]
        public void TestInitialize()
        {
            _connection = new SqliteConnection("Datasource=:memory:");
            _connection.Open();
            _options = new DbContextOptionsBuilder<ConfigAppContext>()
                .UseSqlite(_connection).Options;
            using ConfigAppContext context = new ConfigAppContext(_options);
            context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _connection.Close();
        }

        [TestMethod]
        public void SetLastLocation_InsertsLocationIfNotPresent()
        {
            // Arrange
            var result = 0;
            var before = 0;
            using (ConfigAppContext context = new ConfigAppContext(_options))
            {
                ILocationRepository repo = new LocationRepository(context);
                before = repo.GetAllLocations().Count();
                var location = new LastLocation { EmployeeId = "1234", ReadableLocationName = "first location" };
                // Act
                repo.SetLastLocation(location);

            }
            using (ConfigAppContext context = new ConfigAppContext(_options))
            {
                ILocationRepository repo = new LocationRepository(context);
                //Assert
                result = repo.GetAllLocations().Count();
            }
            Assert.AreEqual(before + 1, result);
        }

        [TestMethod]
        public void SetLastLocation_UpdatesLocationIfPresent()
        {
            // Arrange
            IEnumerable<LastLocation> result;
            var before = 0;
            using (ConfigAppContext context = new ConfigAppContext(_options))
            {
                ILocationRepository repo = new LocationRepository(context);
                before = repo.GetAllLocations().Count();
                var location = new LastLocation { EmployeeId = "1234", ReadableLocationName = "first location" };
                // Act
                repo.SetLastLocation(location);

            }
            using (ConfigAppContext context = new ConfigAppContext(_options))
            {
                ILocationRepository repo = new LocationRepository(context);
                var location = new LastLocation { EmployeeId = "1234", ReadableLocationName = "second location" };
                repo.SetLastLocation(location);
                result = repo.GetAllLocations();
            }
            //Assert
            Assert.AreEqual(before + 1, result.Count());
            Assert.IsTrue(result.Any(l => l.ReadableLocationName == "second location"));
        }

    }
}
