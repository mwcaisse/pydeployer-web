using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Common.ViewModels;
using PyDeployer.Logic.Services;

namespace PyDeployer.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/application/{applicationId:long}/environment/{environmentId:long}/token/")]
    public class ApplicationEnvironmentTokenApiController : Controller
    {

        private readonly ApplicationEnvironmentTokenService _tokenService;

        public ApplicationEnvironmentTokenApiController(ApplicationEnvironmentTokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            return Ok(_tokenService.Get(id));
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll(long applicationId, long environmentId)
        {
            return Ok(_tokenService.GetForEnvironment(applicationId, environmentId));
        }

        [HttpGet]
        [Route("~/api/application/{applicationUuid:string}/environment/{environmentUuid:string}/token/")]
        public IActionResult GetllByUuid(string applicationUuid, string environmentUuid)
        {
            return Ok(_tokenService.GetForEnvironmentByUuid(applicationUuid, environmentUuid));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(long applicationId, long environmentId, 
            [FromBody] ApplicationEnvironmentTokenViewModel token)
        {
            token.ApplicationId = applicationId;
            token.EnvironmentId = environmentId;
            return Ok(_tokenService.Create(token));
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Update(long applicationId, long environmentId, long id,
            [FromBody] ApplicationEnvironmentTokenViewModel token)
        {
            token.ApplicationEnvironmentTokenId = id;
            token.ApplicationId = applicationId;
            token.EnvironmentId = environmentId;
            return Ok(_tokenService.Update(token));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            return Ok(_tokenService.Delete(id));
        }

    }
}