namespace DAC.API.Models.Settings
{
    public class AuthConfiguration
    {
        public string ApiKey { get; set; }

        public JwtConfiguration JWT { get; set; }

    }

    public class JwtConfiguration
    {
        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public string Secret { get; set; }
    }
}
