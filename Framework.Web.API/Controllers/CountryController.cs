﻿using Base.DomainModels;
using Base.Services;
using Framework.DomainModels.Master;
using Framework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly IGenericService genericService;


        public CountryController(IGenericService genericService)
        {
            this.genericService = genericService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("insert")]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> insert([FromQuery] CountryMaster _countryMaster, [FromQuery] CancellationToken cancellationToken)
        {
            ServiceDataResponse<string> serviceResponse = await this.genericService
                .insert(_countryMaster, cancellationToken)
                .ConfigureAwait(false);

            return Ok(serviceResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("update")]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> update([FromQuery] CountryMaster _countryMaster, [FromQuery] CancellationToken cancellationToken)
        {
            ServiceDataResponse<string> serviceResponse = await this.genericService
                .update(_countryMaster, cancellationToken)
                .ConfigureAwait(false);

            return Ok(serviceResponse);
        }


        [HttpPost]
        [Authorize]
        [Route("delete")]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> delete([FromQuery] int id, [FromQuery] CancellationToken cancellationToken)
        {
            ServiceDataResponse<string> serviceResponse = await this.genericService
                .delete(id, cancellationToken)
                .ConfigureAwait(false);

            return Ok(serviceResponse);
        }

        [HttpGet]
        [Authorize]
        [Route("select")]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> select([FromQuery] int id, [FromQuery] CancellationToken cancellationToken)
        {

            ServiceDataResponse<CountryMaster> serviceResponse = await this.genericService
                .Search(id, cancellationToken)
                .ConfigureAwait(false);

            return Ok(serviceResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("selectlist")]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> selectlist([FromQuery] string searchcriteria, [FromQuery] int top, [FromQuery] int skip, CancellationToken cancellationToken)
        {
            ServiceDataResponse<List<CountryMaster>> serviceResponse = await this.genericService
                .SearchList(searchcriteria, top, skip, cancellationToken)
                .ConfigureAwait(false);

            return Ok(serviceResponse);
        }

        [HttpGet]
       // [Authorize]
        [Route("selectstate")]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> selectstate([FromQuery] int id, [FromQuery] CancellationToken cancellationToken)
        {

            ServiceDataResponse<StateMaster> serviceResponse = await this.genericService
                .SearchState(id, cancellationToken)
                .ConfigureAwait(false);

            return Ok(serviceResponse);
        }
    }
}
