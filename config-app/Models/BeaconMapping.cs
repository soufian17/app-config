using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app
{
    public class BeaconMapping
    {
        public long Id { get; set; }
        public string ConnectableName { get; set; }
        public string ProximityUuid { get; set; }
        public string Identifier { get; set; }
        public string ReadableName { get; set; }
        public int Minor { get; set; }
        public int Major { get; set; }
    }
}
