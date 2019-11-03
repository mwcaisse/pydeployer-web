using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Logic.Services;

namespace PyDeployer.Web.Controllers.Api
{

    [Authorize]
    [Produces("application/json")]
    public class ApplicationEnvironmentApiController : BaseApiController
    {

        private readonly ApplicationEnvironmentService _applicationEnvironmentService;

        public ApplicationEnvironmentApiController(ApplicationEnvironmentService applicationEnvironmentService)
        {
            this._applicationEnvironmentService = applicationEnvironmentService;
        }

        [HttpGet]
        [Route("api/application/{applicationId:long}/environment/")]
        public IActionResult GetEnvironmentsForApplication(long applicationId)
        {
            return Ok(_applicationEnvironmentService.GetForApplication(applicationId)
                .Select(ae => ae.Environment).ToList());
        }

        [HttpGet]
        [Route("api/environment/{environmentId:long}/application")]
        public IActionResult GetApplicationsForEnvironment(long environmentId)
        {
            return Ok(_applicationEnvironmentService.GetForEnvironment(environmentId)
                .Select(ae => ae.Application).ToList());
        }

        [HttpPost]
        [Route("api/application/{applicationId:long}/environment/{environmentId:long}")]
        [Route("api/environment/{environmentId:long}/application/{applicationId:long}")]
        public IActionResult Create(long applicationId, long environmentId)
        {
            _applicationEnvironmentService.Create(applicationId, environmentId);
            return Ok(true);
        }

        [HttpDelete]
        [Route("api/application/{applicationId:long}/environment/{environmentId:long}")]
        [Route("api/environment/{environmentId:long}/application/{applicationId:long}")]
        public IActionResult Delete(long applicationId, long environmentId)
        {
            return Ok(_applicationEnvironmentService.Delete(applicationId, environmentId));
        }

    }
}
