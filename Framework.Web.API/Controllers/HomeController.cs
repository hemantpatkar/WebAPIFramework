using Base.DomainModels;
using Framework.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IGenericService genericService;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ILogWriter _logWriter;

        public HomeController(IGenericService genericService, IStringLocalizer<HomeController> localizer, ILogWriter logWriter)
        {
            this.genericService = genericService;
            _localizer = localizer;
            _logWriter = logWriter;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        //[Authorize]
        [ProducesResponseType(typeof(EntityPaginatedSet<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public string Get()
        {
            _logWriter.Info("Get");
            _logWriter.Warning("Get");
            _logWriter.Error("Get");
            return "Welcome to basic auth api";
        }

        [HttpPost("UploadFile")]
        public async Task<string> UploadFile([FromForm] IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string fName = file.FileName;
                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  
                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                var extension = ext.ToLower();
                if (!AllowedFileExtensions.Contains(extension))
                {
                    return _localizer["jpgerror"];
                }
                else if (file.Length > MaxContentLength)
                {
                    return string.Format("Please Upload a file upto 1 mb.");
                }
                else
                {
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "/" + file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            return file.FileName;
        }

    }
}
