using Base.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Base.Services;
using Framework.Services;

namespace Framework.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IGenericService genericService;

        public HomeController(IGenericService genericService)
        {
            this.genericService = genericService;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public string Get()
        {
            return "Welcome to basic auth api";
        }

        /// <summary>
        /// Retrieves the employee shortlistings.
        /// </summary>
        /// <param name="employeeFilterCriteria">The employee filter criteria.</param>
        /// <param name="top">The number of records to fetch.</param>
        /// <param name="skip">The number of records to skip.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// An <see cref="ActionResult" /> containing the employee shortlisting or a <see cref="NotFoundResult" /> if the entry is not found.
        /// </returns>
        [HttpPost]
        //[Authorize]
        [Route("SearchEmployees")]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> InsertLog([FromQuery] string log, [FromQuery] int top, [FromQuery] int skip, CancellationToken cancellationToken)
        {
            ServiceDataResponse<string> serviceResponse = await this.genericService
                .SearchEmployees(log, top, skip, cancellationToken)
                .ConfigureAwait(false);

            return Ok(serviceResponse);
        }
    }
}
