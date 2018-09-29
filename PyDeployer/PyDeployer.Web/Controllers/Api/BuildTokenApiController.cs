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
    [Route("api/build/token")]
    public class BuildTokenApiController : BaseApiController
    {

        private readonly BuildTokenService _buildTokenService;

        public BuildTokenApiController(BuildTokenService buildTokenService)
        {
            this._buildTokenService = buildTokenService;
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            return Ok(_buildTokenService.Get(id));
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(_buildTokenService.GetAll());
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] BuildTokenViewModel token)
        {
            return Ok(_buildTokenService.Create(token));
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Update(long id, [FromBody] BuildTokenViewModel token)
        {
            token.BuildTokenId = id;
            return Ok(_buildTokenService.Update(token));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            _buildTokenService.Delete(id);
            return Ok();
        }



    }
}
