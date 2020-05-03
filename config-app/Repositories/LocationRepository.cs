using config_app.Controllers;
using config_app.DAL;
using config_app.Models;
using config_app.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public LocationRepository(ConfigAppContext context)
        {
            _context = context;
        }

        private readonly ConfigAppContext _context;

        public void SetLastLocation(LastLocation location)
        {
            var lastLocation = _context.LastLocations.Where(l => l.EmployeeId == location.EmployeeId).FirstOrDefault();
            if (lastLocation != null)
            {
                lastLocation.ReadableLocationName = location.ReadableLocationName;
            }
            else
            {
                _context.LastLocations.Add(location);
            }
            _context.SaveChanges();
        }

        public IEnumerable<LastLocation> GetAllLocations()
        {
            return _context.LastLocations.ToList();
        }
    }
}
