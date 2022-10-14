using CoreLib.Crypto;
using CoreLib.Sql;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public static async Task<ISqlResult> ExecuteStoreAsync(this ISqlHandler sqlHandler, string store, IEnumerable<SqlParameter> sqlParameters)
        {
            var sqlResult = await sqlHandler.WriteDataAsync(true, store, sqlParameters);
            if (sqlResult.Success)
            {
                var tryCatchErrorMessage = sqlResult.Outputs.GetValue<string>("error_message");
                if (!string.IsNullOrEmpty(tryCatchErrorMessage))
                {
                    sqlResult.Exception = new Exception(tryCatchErrorMessage);
                    sqlResult.Success = false;
                }
            }
            return sqlResult;
        }
    }
}
