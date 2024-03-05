namespace Cards.Cache.Configurations
{
    public class RedisCacheSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Database { get; set; } = 0;
    }
}
