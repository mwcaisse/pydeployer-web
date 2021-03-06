﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Common.Mappers;
using PyDeployer.Common.ViewModels;
using PyDeployer.Logic.Services;

namespace PyDeployer.Web.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/application")]
    public class ApplicationApiController : BaseApiController
    {

        private readonly ApplicationService _applicationService;

        public ApplicationApiController(ApplicationService applicationService)
        {
            this._applicationService = applicationService;
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            return Ok(_applicationService.Get(id));
        }

        [HttpGet]
        [Route("{uuid}")]
        public IActionResult GetByUuid(string uuid)
        {
            return Ok(_applicationService.GetByUuid(uuid));
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(_applicationService.GetAll());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] ApplicationViewModel application)
        {
            
            return Ok(_applicationService.Create(application));
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Update(long id, [FromBody] ApplicationViewModel application)
        {
            application.ApplicationId = id;
            return Ok(_applicationService.Update(application));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            return Ok(_applicationService.Delete(id));
        }

    }
}
