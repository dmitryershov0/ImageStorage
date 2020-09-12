using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStorage.Services
{
	public interface IStorage
	{
		byte[] GetImage(Guid id);

		string GetImagePath(Guid id);

		Guid SaveImage(Guid parrentId, byte[] image);

		bool DeleteImage(Guid id);
	}
}
