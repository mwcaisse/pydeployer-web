using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PyDeployer.Web.Configuration;

namespace PyDeployer.Web.Controllers.View
{
    [Route("build-token")]
    public class BuildTokenController : BaseViewController
    {
        public BuildTokenController(ApplicationConfiguration applicationConfiguration) : base(applicationConfiguration)
        {
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
