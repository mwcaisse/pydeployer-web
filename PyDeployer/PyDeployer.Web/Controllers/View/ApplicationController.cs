using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Web.Configuration;
using PyDeployer.Web.Models;

namespace PyDeployer.Web.Controllers.View
{
    [Route("/application")]
    public class ApplicationController : BaseViewController
    {
        public ApplicationController(ApplicationConfiguration applicationConfiguration) 
            : base(applicationConfiguration)
        {
        }

        [Authorize]
        [Route("{id:long}")]
        public IActionResult Index(long id)
        {
            return VueView("views/ApplicationDetail", "Application Details", new VueViewProperty()
            {
                Name = "applicationId",
                Value = id.ToString()
            });
        }
    }
}