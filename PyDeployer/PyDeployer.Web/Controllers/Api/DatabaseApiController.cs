using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PyDeployer.Common.ViewModels;
using PyDeployer.Logic.Services;

namespace PyDeployer.Web.Controllers.Api
{
    
    [Authorize]
    [Produces("application/json")]
    [Route("api/environment/{environmentId:long}/database")]
    public class DatabaseApiController : BaseApiController
    {
        
        private readonly DatabaseService _databaseService;

        public DatabaseApiController(DatabaseService databaseService)
        {
            this._databaseService = databaseService;
        }
        
        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var database = _databaseService.Get(id);
            if (null == database)
            {
                return NotFound();
            }
            return Ok(database);
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll(long environmentId)
        {
            return Ok(_databaseService.GetAllForEnvironment(environmentId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(long environmentId, [FromBody] DatabaseViewModel toCreate)
        {
            toCreate.EnvironmentId = environmentId;
            return Ok(_databaseService.Create(toCreate));
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Update(long id, long environmentId, [FromBody] DatabaseViewModel toUpdate)
        {
            toUpdate.DatabaseId = id;
            toUpdate.EnvironmentId = environmentId;
            return Ok(_databaseService.Update(toUpdate));
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            _databaseService.Delete(id);
            return NoContent();
        }
        
    }
}