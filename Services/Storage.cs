using System;
using System.IO;
using ImageStorage.Models;
using Newtonsoft.Json;

namespace ImageStorage.Services
{
	/// <inheritdoc cref="IStorage"/>
	public class Storage : IStorage
	{
		private IRedis _redis;
		private Guid CreateKey => 
			Guid.NewGuid();

		/// <summary>
		/// Storage path
		/// </summary>
		public string StoragePath =>
			Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Storage");

		public Storage(IRedis redis)
		{
			_redis = redis; ;
		}

		/// <inheritdoc cref="IStorage.SaveImage(Guid, byte[])"/>
		public Guid SaveImage(Guid parrentId, byte[] image)
		{
			var item = new StorageItem
			{
				ParrentId = parrentId,
				Id = CreateKey
			};

			File.WriteAllBytes(Path.Combine(StoragePath, item.Id.ToString()), image);
			
			_redis.Database.StringSet(item.Id.ToString(), JsonConvert.SerializeObject(item));

			return item.Id;
		}

		/// <inheritdoc cref="IStorage.DeleteImage(Guid)"/>
		public bool DeleteImage(Guid id)
		{
			var key = id.ToString();
			File.Delete(Path.Combine(StoragePath, key));
			return _redis.Database.KeyDelete(key);
		}

		/// <inheritdoc cref="IStorage.GetImagePath(Guid)"/>
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

		/// <inheritdoc cref="IStorage.GetImage(Guid)"/>
		public byte[] GetImage(Guid id)
		{
			var path = GetImagePath(id);
			return File.ReadAllBytes(path);
		}

		/// <inheritdoc cref="IStorage.GetPreviosImage(Guid)"/>
		public byte[] GetPreviosImage(Guid id)
		{
			var redisItem = _redis.Database.StringGet(id.ToString());
			var item = JsonConvert.DeserializeObject<StorageItem>(redisItem);
			return GetImage(item.ParrentId);
		}
	}
}
