using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Models
{
    public class LastLocation
    {
        public long Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ReadableLocationName { get; set; }
    }
}
