using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class MailTemplate
    {
        static string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailTemplates");

        static string readContent(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"File '{filePath}' was not found");

                using (var sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                if (ex is ResponseException)
                    throw ex;

                throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, ex.Message);
            }
        }

        public static string EmailConfirmation
        {
            get
            {
                var filePath = Path.Combine(folderPath, "email-confirmation.html");
                return readContent(filePath);
            }
        }

        public static string PasswordReset
        {
            get
            {
                var filePath = Path.Combine(folderPath, "password-reset.html");
                return readContent(filePath);
            }
        }
    }
}
