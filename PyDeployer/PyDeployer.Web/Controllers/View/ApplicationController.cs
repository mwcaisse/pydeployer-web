using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PyDeployer.Web.Controllers.View
{
    public class ApplicationController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}