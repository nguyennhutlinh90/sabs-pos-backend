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
                //var appSettings = _appSettings;
                //appSettings.Configuration.ConnectionString = _appSettings.Configuration.ConnectionString.Decrypt();
                //appSettings.SeedData.AccountName = _appSettings.SeedData.AccountName.Decrypt();
                //appSettings.SeedData.AccountEmail = _appSettings.SeedData.AccountEmail.Decrypt();
                //appSettings.SeedData.AccountPass = _appSettings.SeedData.AccountPass.Decrypt();
                //appSettings.LinkSettings.ConfirmEmail1 = _appSettings.LinkSettings.ConfirmEmail1.Decrypt();
                //appSettings.LinkSettings.ResetPassword = _appSettings.LinkSettings.ResetPassword.Decrypt();
                //appSettings.MailSettings.Sender = _appSettings.MailSettings.Sender.Decrypt();
                //appSettings.MailSettings.SmtpHost = _appSettings.MailSettings.SmtpHost.Decrypt();
                //appSettings.MailSettings.SmtpUser = _appSettings.MailSettings.SmtpUser.Decrypt();
                //appSettings.MailSettings.SmtpPass = _appSettings.MailSettings.SmtpPass.Decrypt();
                //return appSettings;

                return _appSettings.Configuration.ConnectionString.Decrypt();
            });
        }
    }
}
