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
}
