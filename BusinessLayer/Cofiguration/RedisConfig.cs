using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace BusinessLayer.Cofiguration
{
    public class RedisConfig
    {
        private readonly ConnectionMultiplexer _redis;
        public StackExchange.Redis.IDatabase Database { get; }

        public RedisConfig(IConfiguration configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]);
            Database = _redis.GetDatabase();
        }
    }
}
