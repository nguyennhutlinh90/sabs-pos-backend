using CoreLib.Crypto;

using Microsoft.Extensions.Configuration;

namespace sabs_pos_backend_api
{
    public static class OtherExtension
    {
        public static AppSettings GetSettings(this IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();
            appSettings.Configuration.ConnectionString = AesCrypto.Decrypt(appSettings.Configuration.ConnectionString);
            appSettings.SeedData.AccountName = AesCrypto.Decrypt(appSettings.SeedData.AccountName);
            appSettings.SeedData.AccountEmail = AesCrypto.Decrypt(appSettings.SeedData.AccountEmail);
            appSettings.SeedData.AccountPass = AesCrypto.Decrypt(appSettings.SeedData.AccountPass);
            appSettings.LinkSettings.EmailConfirmation = AesCrypto.Decrypt(appSettings.LinkSettings.EmailConfirmation);
            appSettings.LinkSettings.ResetPassword = AesCrypto.Decrypt(appSettings.LinkSettings.ResetPassword);
            appSettings.MailSettings.Sender = AesCrypto.Decrypt(appSettings.MailSettings.Sender);
            appSettings.MailSettings.SmtpHost = AesCrypto.Decrypt(appSettings.MailSettings.SmtpHost);
            appSettings.MailSettings.SmtpUser = AesCrypto.Decrypt(appSettings.MailSettings.SmtpUser);
            appSettings.MailSettings.SmtpPass = AesCrypto.Decrypt(appSettings.MailSettings.SmtpPass);
            return appSettings;
        }
    }
}
