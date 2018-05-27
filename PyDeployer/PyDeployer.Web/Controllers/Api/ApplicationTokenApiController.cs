using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PyDeployer.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/application/{applicationId:long}/token/")]
    public class ApplicationTokenApiController : Controller
    {

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long applicationId, long id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll(long applicationId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(long applicationId)
        {
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update(long applicationId)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long applicationId, long id)
        {
            return Ok();
        }

    }
}
