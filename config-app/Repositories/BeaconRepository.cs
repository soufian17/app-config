using config_app.DAL;
using config_app.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Repositories
{
    public class BeaconRepository : IBeaconRepository
    {
        public BeaconRepository(ConfigAppContext context)
        {
            _context = context;
        }

        private readonly ConfigAppContext _context;

        public IEnumerable<BeaconMapping> GetAllBeaconMappings()
        {
           return _context.BeaconMappings.ToList();
        }
    }
}
