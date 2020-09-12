using StackExchange.Redis;

namespace ImageStorage.Services
{
	public class Redis : IRedis
	{
		private IConnectionMultiplexer _redis;
		public IDatabase Database => _redis.GetDatabase();
		public Redis(string host, string port)
		{
			var configString = $"{host}:{port},connectRetry=5";
			_redis = ConnectionMultiplexer.Connect(configString);
		}
	}
}
