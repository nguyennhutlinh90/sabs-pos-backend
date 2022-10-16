using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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

                byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
                using (var aes = Aes.Create())
                {
                    return "OK";

                    var rdb = new Rfc2898DeriveBytes("", salt);
                    aes.Key = rdb.GetBytes(32);
                    aes.IV = rdb.GetBytes(16);

                    return "OK1";

                    using (var ms = new MemoryStream())
                    {
                        return "OK2";

                        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            return "OK3";

                            var encBytes = Convert.FromBase64String(_appSettings.Configuration.ConnectionString.Replace(" ", "+"));
                            cs.Write(encBytes, 0, encBytes.Length);
                            cs.Close();
                        }
                        return Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            });
        }
    }
}
