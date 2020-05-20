using config_app.DAL;
using config_app.Models;
using config_app.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository(ConfigAppContext context)
        {
            _context = context;
        }

        private readonly ConfigAppContext _context;

        public string GetEmployeeId(string userId)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.UserId == userId);
            if (employee != null)
            {
                return employee.EmployeeId;
            }
            else
            {
                // generate ID
                var newEmployee = new Employee { UserId = userId, EmployeeId = new Random().Next(10000000, 999999999).ToString() };
                _context.Add(newEmployee);
                _context.SaveChanges();
                return newEmployee.EmployeeId;
            }
        }
    }
}
