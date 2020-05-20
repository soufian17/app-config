using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace config_app.Controllers
{
    [Route("apple-app-site-association")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Json(
                new {
                    applinks = new
                    {
                        apps = new List<string>(),
                        details = new List<object> 
                        {
                            new
                            {
                                appID = "BDCC64DUG6.infosupport.soufiant.Info-Card",
                                paths= new List<string> { "*" }
                            }
                        }
                    }
            }
            );
        }
    }
}