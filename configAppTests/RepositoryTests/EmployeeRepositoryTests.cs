using config_app.DAL;
using config_app.Models;
using config_app.Repositories;
using config_app.Repositories.Abstractions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace configAppTests.RepositoryTests
{
    [TestClass]
    public class EmployeeRepositoryTests
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
        public void GetEmployeeData_ReturnsNewEmployeeIfNotExists()
        {
            // Arrange
            using ConfigAppContext context = new ConfigAppContext(_options);
            IEmployeeRepository repo = new EmployeeRepository(context);
            // Act
            var result = repo.GetEmployeeData("test");
            //Assert
            Assert.AreEqual(result.UserId, "test");
            Assert.AreEqual(result.ValidUntil, DateTime.MaxValue);
            Assert.AreEqual(result.Role, EmployeeRole.Employee);
        }

        [TestMethod]
        public void GetEmployeeData_ReturnsEmployeeIfExists()
        {
            // Arrange
            using ConfigAppContext context = new ConfigAppContext(_options);
            IEmployeeRepository repo = new EmployeeRepository(context);
            var insert = repo.GetEmployeeData("test");
            // Act
            var result = repo.GetEmployeeData("test");
            //Assert
            Assert.AreEqual(result.UserId, "test");
            Assert.AreEqual(insert.EmployeeId, result.EmployeeId);
            Assert.AreEqual(result.ValidUntil, DateTime.MaxValue);
            Assert.AreEqual(result.Role, EmployeeRole.Employee);
        }

        [TestMethod]
        public void AddVisitor_AddsAVisitorAccountUsableForOneDay()
        {
            // Arrange
            using ConfigAppContext context = new ConfigAppContext(_options);
            IEmployeeRepository repo = new EmployeeRepository(context);
            var expiryDate = DateTime.Now.AddDays(1);
            repo.AddVisitorAccount("test", expiryDate);
            // Act
            var result = repo.GetEmployeeData("test");
            //Assert
            Assert.AreEqual(result.UserId, "test");
            Assert.AreEqual(result.Role, EmployeeRole.Visitor);
            Assert.AreEqual(result.ValidUntil, expiryDate);
        }
        [TestMethod]
        public void GetEmployeeData_ReturnsNullIfAccountIsExpired()
        {
            // Arrange
            using ConfigAppContext context = new ConfigAppContext(_options);
            IEmployeeRepository repo = new EmployeeRepository(context);
            repo.AddVisitorAccount("test", DateTime.Now.AddDays(-1));
            // Act
            var result = repo.GetEmployeeData("test");
            //Assert
            Assert.AreEqual(result, null);
        }
    }
}
