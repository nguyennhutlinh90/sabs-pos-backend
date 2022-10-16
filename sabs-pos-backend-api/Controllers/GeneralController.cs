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
                var appSettings = _appSettings;
                //appSettings.Configuration.ConnectionString = AesCrypto.Decrypt(_appSettings.Configuration.ConnectionString);
                //appSettings.SeedData.AccountName = AesCrypto.Decrypt(_appSettings.SeedData.AccountName);
                //appSettings.SeedData.AccountEmail = AesCrypto.Decrypt(_appSettings.SeedData.AccountEmail);
                //appSettings.SeedData.AccountPass = AesCrypto.Decrypt(_appSettings.SeedData.AccountPass);
                //appSettings.LinkSettings.EmailConfirmation = AesCrypto.Decrypt(_appSettings.LinkSettings.EmailConfirmation);
                //appSettings.LinkSettings.ResetPassword = AesCrypto.Decrypt(_appSettings.LinkSettings.ResetPassword);
                //appSettings.MailSettings.Sender = AesCrypto.Decrypt(_appSettings.MailSettings.Sender);
                //appSettings.MailSettings.SmtpHost = AesCrypto.Decrypt(_appSettings.MailSettings.SmtpHost);
                //appSettings.MailSettings.SmtpUser = AesCrypto.Decrypt(_appSettings.MailSettings.SmtpUser);
                //appSettings.MailSettings.SmtpPass = AesCrypto.Decrypt(_appSettings.MailSettings.SmtpPass);
                return appSettings;
            });
        }
    }
}
