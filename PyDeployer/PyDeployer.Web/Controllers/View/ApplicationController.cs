using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Web.Configuration;

namespace PyDeployer.Web.Controllers.View
{
    public class ApplicationController : BaseViewController
    {
        public ApplicationController(ApplicationConfiguration applicationConfiguration) 
            : base(applicationConfiguration)
        {
        }

        [Authorize]
        public IActionResult Index()
        {
            return VueView("views/ApplicationDetail", "Application Details");
        }
    }
}