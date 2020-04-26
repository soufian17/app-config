using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace config_app.Controllers
{
    [ApiController]
    [Route("beacon-mapping")]
    public class BeaconMappingController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<BeaconMapping> Get()
        {
            return new BeaconMapping[] 
            { 
                new BeaconMapping {
                    ConnectableName="HMSoft",
                    ProximityUuid="644f76f7-6a52-42bc-e911-fd902c9bb987",
                    Identifier="",
                }
            };
        }
    }
}