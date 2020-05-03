using config_app.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.DAL
{

    public class ConfigAppContext: DbContext
    {
        public ConfigAppContext(DbContextOptions<ConfigAppContext> contextOptions): base(contextOptions)
        {

        }

        public DbSet<BeaconMapping> BeaconMappings { get; set; }
        public DbSet<LastLocation> LastLocations { get; set; }
    }
}
