using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Logic.Services;

namespace PyDeployer.Web.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/application/{applicationId:long}/environment/")]
    public class ApplicationEnvironmentApiController : Controller
    {

        private readonly ApplicationEnvironmentService _applicationEnvironmentService;

        public ApplicationEnvironmentApiController(ApplicationEnvironmentService applicationEnvironmentService)
        {
            this._applicationEnvironmentService = applicationEnvironmentService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get(long applicationId)
        {
            return Ok(_applicationEnvironmentService.GetForApplication(applicationId)
                .Select(ae => ae.Environment).ToList());
        }

        [HttpPost]
        [Route("{environmentId:long}")]
        public IActionResult Create(long applicationId, long environmentId)
        {
            _applicationEnvironmentService.Create(applicationId, environmentId);
            return Ok(true);
        }

        [HttpDelete]
        [Route("{environmentId:long}")]
        public IActionResult Delete(long applicationId, long environmentId)
        {
            return Ok(_applicationEnvironmentService.Delete(applicationId, environmentId));
        }

    }
}
