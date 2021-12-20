using Base.DomainModels;
using Framework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Reflection;

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


        [HttpPost("UploadFile")]
        public async Task<string> UploadFile([FromForm] IFormFile file)
        {
            string fName = file.FileName;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images/" + file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return file.FileName;
        }

    }
}
