
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Web.Configuration;

namespace PyDeployer.Web.Controllers.View
{
    [Route("environment/")]
    public class EnvironmentController : BaseViewController
    {

        public EnvironmentController(ApplicationConfiguration applicationConfiguration) 
            : base(applicationConfiguration)  { }

        /*
        [Authorize]
        [Route("{id:long}")]
        public IActionResult Index(long id)
        {
            return View();
        }*/
      
        
    }
}