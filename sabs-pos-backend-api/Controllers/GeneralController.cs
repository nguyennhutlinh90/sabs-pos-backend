using CoreLib.Crypto;

using Microsoft.AspNetCore.Mvc;

namespace sabs_pos_backend_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]
    public class GeneralController : ActionResultBase
    {
        readonly IAppSettings _appSettings;
        public GeneralController(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet]
        [ApiVersionNeutral]
        [Route("settings")]
        public IActionResult Settings()
        {
            return Execute(() =>
            {
                return _appSettings.Configuration.ConnectionString.Decrypt();
            });
        }
    }
}
