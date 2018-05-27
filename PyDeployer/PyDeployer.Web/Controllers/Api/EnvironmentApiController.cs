using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Common.ViewModels;

namespace PyDeployer.Web.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/environment")]
    public class EnvironmentApiController : Controller
    {

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{uuid:string}")]
        public IActionResult GetByUuid(string uuid)
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
        public IActionResult Create([FromBody] EnvironmentViewModel environment)
        {
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody] EnvironmentViewModel environment)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            return Ok();
        }

    }
}
