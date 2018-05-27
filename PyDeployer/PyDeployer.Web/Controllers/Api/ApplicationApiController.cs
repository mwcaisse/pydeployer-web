using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Common.ViewModels;

namespace PyDeployer.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/application")]
    public class ApplicationApiController : Controller
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
        public IActionResult Create([FromBody] ApplicationViewModel application)
        {
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update([FromBody] ApplicationViewModel application)
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
