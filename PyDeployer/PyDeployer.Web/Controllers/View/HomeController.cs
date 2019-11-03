using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Web.Configuration;

namespace PyDeployer.Web.Controllers.View
{
    public class HomeController : BaseViewController
    {
        public HomeController(ApplicationConfiguration applicationConfiguration) 
            : base(applicationConfiguration)
        {
        }

        [Authorize]
        public IActionResult Index()
        {
            return VueView("views/Home", "Index");
        }
    }
}