using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;
using MimeKit.Text;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class MailService : ServiceBase, IMailService
    {
        readonly IAppSettings _appSettings;
        public MailService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task Send(string subject, string body, params string[] receivers)
        {
            try
            {
                if (receivers == null)
                    return;

                receivers = receivers.Where(r => !string.IsNullOrEmpty(r)).ToArray();
                if (receivers.Length <= 0)
                    return;

                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_appSettings.MailSettings.Sender));
                email.To.AddRange(receivers.Select(r => MailboxAddress.Parse(r)));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = body };

                // send email
                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(_appSettings.MailSettings.SmtpHost, _appSettings.MailSettings.SmtpPort, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_appSettings.MailSettings.SmtpUser, _appSettings.MailSettings.SmtpPass);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new ResponseException(ResponseCode.INTERNAL_SERVER_ERROR, ex.Message);
            }
        }
    }
}
