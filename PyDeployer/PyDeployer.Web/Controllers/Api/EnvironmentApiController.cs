using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Common.ViewModels;
using PyDeployer.Logic.Services;

namespace PyDeployer.Web.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/environment")]
    public class EnvironmentApiController : BaseApiController
    {

        private readonly EnvironmentService _environmentService;

        public EnvironmentApiController(EnvironmentService environmentService)
        {
            this._environmentService = environmentService;
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            return Ok(_environmentService.Get(id));
        }

        [HttpGet]
        [Route("{uuid}")]
        public IActionResult GetByUuid(string uuid)
        {
            return Ok(_environmentService.GetByUuid(uuid));
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(_environmentService.GetAll());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] EnvironmentViewModel environment)
        {
            return Ok(_environmentService.Create(environment));
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Update(long id, [FromBody] EnvironmentViewModel environment)
        {
            environment.EnvironmentId = id;
            return Ok(_environmentService.Update(environment));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            return Ok(_environmentService.Delete(id));
        }

    }
}
