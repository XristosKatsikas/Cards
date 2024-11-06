namespace Cards.Domain.Configurations
{
    public class JwtTokenSettings
    {
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public string JwtRegisteredClaimNamesSub { get; set; } = string.Empty;
        public int ExpirationDays { get; set; }
    }
}