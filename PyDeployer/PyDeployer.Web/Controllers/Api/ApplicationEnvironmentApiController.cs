using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PyDeployer.Web.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/application/{applicationId:long}/environment/")]
    public class ApplicationEnvironmentApiController : Controller
    {
        
        [HttpGet]
        [Route("")]
        public IActionResult Get(long applicationId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("{environmentId:long}")]
        public IActionResult Create(long applicationId, long environmentId)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{environmentId:long}")]
        public IActionResult Delete(long applicationId, long environmentId)
        {
            return Ok();
        }

    }
}
