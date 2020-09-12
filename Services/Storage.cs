using System;
using StackExchange.Redis;
using System.IO;
using ImageStorage.Models;
using Newtonsoft.Json;

namespace ImageStorage.Services
{

	public class Storage : IStorage
	{
		private ConnectionMultiplexer redis;
		private IDatabase db;
		private Guid CreateKey => 
			Guid.NewGuid();
		protected IDatabase database => db ??
			(db = redis.GetDatabase());

		public string StoragePath =>
			Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Storage");

		public Storage(string address = "localhost")
		{
			redis = ConnectionMultiplexer.Connect(address);
		}


		public Guid SaveImage(Guid parrentId, byte[] image)
		{
			var item = new StorageItem
			{
				ParrentId = parrentId,
				Id = CreateKey
			};

			File.WriteAllBytes(Path.Combine(StoragePath, item.Id.ToString()), image);

			database.StringSet(item.Id.ToString(), JsonConvert.SerializeObject(item));

			return item.Id;
		}

		public bool DeleteImage(Guid id)
		{
			var key = id.ToString();
			File.Delete(Path.Combine(StoragePath, key));
			return database.KeyDelete(key);
		}

		public string GetImagePath(Guid id)
		{
			var path = Path.Combine(StoragePath, id.ToString());
			if (File.Exists(path))
				return path;
			else
			{
				return Path.Combine(StoragePath, Guid.Empty.ToString());
			}
		}

		public byte[] GetImage(Guid id)
		{
			var path = GetImagePath(id);
			return File.ReadAllBytes(path);
		}
	}
}
