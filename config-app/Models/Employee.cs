using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Models
{
    public class Employee
    {
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime ValidUntil { get; set; }
        public EmployeeRole Role { get; set; }

        public static string GenerateEmployeeId()
        {
            return new Random().Next(10000000, 999999999).ToString();
        }
    }

    public enum EmployeeRole { Employee, Secretary, Visitor }
}
