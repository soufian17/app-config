using config_app;
using config_app.DAL;
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
    public class BeaconRepositoryTests
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
        public void GetAllBeaconMappings_ReturnsEmptyListIfEmpty()
        {
            // Arrange
            using ConfigAppContext context = new ConfigAppContext(_options);
            IBeaconRepository repo = new BeaconRepository(context);
            // Act
            var result = repo.GetAllBeaconMappings();
            //Assert
            Assert.IsFalse(result.Any());
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetAllBeaconMappings_ReturnsListIfData()
        {
            // Arrange
            using ConfigAppContext context = new ConfigAppContext(_options);
            IBeaconRepository repo = new BeaconRepository(context);
            var beaconMappings = new BeaconMapping[]
            {
                new BeaconMapping{ConnectableName="someName",ReadableName="name"},
                new BeaconMapping{ConnectableName="someName2",ReadableName="name2"},
            };
            TestHelper.InjectData(_options, beaconMappings);
            // Act
            var result = repo.GetAllBeaconMappings().ToList();
            //Assert
            Assert.IsTrue(result.Any(b => b.ConnectableName == beaconMappings[0].ConnectableName));
            Assert.IsTrue(result.Any(b => b.ConnectableName == beaconMappings[1].ConnectableName));

        }

    }
}
