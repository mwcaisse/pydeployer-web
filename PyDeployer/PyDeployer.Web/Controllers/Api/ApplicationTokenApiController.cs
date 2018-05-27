using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Common.ViewModels;
using PyDeployer.Logic.Services;

namespace PyDeployer.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/application/{applicationId:long}/token/")]
    public class ApplicationTokenApiController : Controller
    {

        private readonly ApplicationTokenService _applicationTokenService;

        public ApplicationTokenApiController(ApplicationTokenService applicationTokenService)
        {
            this._applicationTokenService = applicationTokenService;
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long applicationId, long id)
        {
            return Ok(_applicationTokenService.Get(id));
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll(long applicationId)
        {
            return Ok(_applicationTokenService.GetForApplication(applicationId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(long applicationId, [FromBody] ApplicationTokenViewModel token)
        {
            token.ApplicationId = applicationId;
            return Ok(_applicationTokenService.Create(token));
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Update(long applicationId, long id, 
            [FromBody] ApplicationTokenViewModel token)
        {
            token.ApplicationTokenId = id;
            token.ApplicationId = applicationId;
            return Ok(_applicationTokenService.Update(token));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long applicationId, long id)
        {
            return Ok(_applicationTokenService.Delete(id));
        }

    }
}
