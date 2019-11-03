
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlTin.Common.Entities;
using PyDeployer.Web.Configuration;
using PyDeployer.Web.Models;

namespace PyDeployer.Web.Controllers.View
{
    [Route("environment/")]
    public class EnvironmentController : BaseViewController
    {

        public EnvironmentController(ApplicationConfiguration applicationConfiguration) 
            : base(applicationConfiguration)  { }

        
        [Authorize]
        [Route("{id:long}")]
        public IActionResult Index(long id)
        {
            return VueView("views/EnvironmentDetail", "Environment Details", new VueViewProperty()
            {
                Name = "environmentId",
                Value = id.ToString()
            });
        }
      
        
    }
}