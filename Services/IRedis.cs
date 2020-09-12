using StackExchange.Redis;
namespace ImageStorage.Services
{
	public interface IRedis
	{
		IDatabase Database { get; }
	}
}
