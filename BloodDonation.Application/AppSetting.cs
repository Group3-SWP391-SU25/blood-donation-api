namespace BloodDonation.Application
{
    public class AppSetting
    {
        public Dictionary<string, string> ConnectionStrings { get; set; } = [];
        public FirebaseConfiguration FirebaseConfig { get; set; } = new();
    }
    public class FirebaseConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
        public string ServiceAccountUsr { get; set; } = string.Empty;
        public string ServiceAccountPwd { get; set; } = string.Empty;
        public string Bucket { get; set; } = string.Empty;
    }
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
