using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mitchell.Authentication.Services;
using Mitchell.Common.ViewModels;

namespace PyDeployer.Web.Controllers.Api
{
    [Authorize]
    [Route("api/user/token")]
    public class UserAuthenticationTokenApiController : BaseApiController
    {

        private readonly IUserAuthenticationTokenService _tokenService;

        public UserAuthenticationTokenApiController(IUserAuthenticationTokenService tokenService)
        {
            this._tokenService = tokenService;
        }

        [HttpGet]
        [Route("active")]
        public IActionResult GetActiveForUser(int skip = DefaultSkip, int take = DefaultTake,
            SortParam sort = null)
        {
            return Ok(_tokenService.GetActiveForUser(skip, take, sort));
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateToken([FromBody]string description)
        {
            return Ok(_tokenService.CreateToken(description));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult DeleteToken(long id)
        {
            return Ok(_tokenService.DeleteToken(id));
        }

    }
}
