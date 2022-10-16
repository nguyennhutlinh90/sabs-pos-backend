namespace sabs_pos_backend_api
{
    public interface IAppSettings
    {
        Configuration Configuration { get; set; }

        SeedData SeedData { get; set; }

        LinkSettings LinkSettings { get; set; }

        MailSettings MailSettings { get; set; }
    }

    public class AppSettings : IAppSettings
    {
        public Configuration Configuration { get; set; }

        public SeedData SeedData { get; set; }

        public LinkSettings LinkSettings { get; set; }

        public MailSettings MailSettings { get; set; }
    }

    public class Configuration
    {
        public string ConnectionString { get; set; }

        public string JwtTokenKey { get; set; }
    }

    public class SeedData
    {
        public string AccountName { get; set; } = "kJuxnU9JBzZvfisEd9aRdQ==";

        public string AccountEmail { get; set; } = "WPwYUj3k0piW7jkKb50ru1eoLuFpV2gapss21VR668s=";

        public string AccountPass { get; set; } = "QWlb6ltUvLrT5abqoRPgHKBJHRqQtp5DxeQk7Pn63dc=";
    }

    public class LinkSettings
    {
        public string ConfirmEmail1 { get; set; }

        public string ResetPassword { get; set; }
    }

    public class MailSettings
    {
        public string Sender { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPass { get; set; }
    }
}
