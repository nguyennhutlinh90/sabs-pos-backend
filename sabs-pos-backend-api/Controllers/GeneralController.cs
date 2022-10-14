using Microsoft.AspNetCore.Mvc;

using System.Reflection;

namespace sabs_pos_backend_api.Controllers
{
    [ApiController]
    [Route("")]
    public class GeneralController : ActionResultBase
    {
        [HttpGet]
        [ApiVersionNeutral]
        [Route("info")]
        public IActionResult Info()
        {
            return Execute(() =>
            {
                var info = new
                {
                    product = "SABS POS Backend API",
                    version = getVersion(),
                    poweredBy = "Delfi Technologies A/S"
                };

                var logs = new object[] {
                    new {
                        version = "1.0.0",
                        issues = new string[] { "#Commit sources" }
                    }
                };

                return new { info, logs };
            });
        }

        string getVersion()
        {
            var assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
            return assemblyVersion.ToString();
        }
    }
}
