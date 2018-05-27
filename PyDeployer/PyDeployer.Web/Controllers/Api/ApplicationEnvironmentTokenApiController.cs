using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PyDeployer.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/application/{applicationId:long}/environment/{environmentId:long}/token/")]
    [Route("api/application/{applicationUuid:string}/environment/{environmentUuid:string}/token/")]
    public class ApplicationEnvironmentTokenApiController : Controller
    {

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            return Ok();
        }

    }
}