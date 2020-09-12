using System;

namespace ImageStorage.Services
{
	/// <summary>
	/// Storage interface
	/// </summary>
	public interface IStorage
	{
		/// <summary>
		/// Get image
		/// </summary>
		/// <param name="id">id image</param>
		/// <returns></returns>
		byte[] GetImage(Guid id);

		/// <summary>
		/// Get previos image
		/// </summary>
		/// <param name="id">id image</param>
		/// <returns></returns>
		byte[] GetPreviosImage(Guid id);

		/// <summary>
		/// Get image path
		/// </summary>
		/// <param name="id">id image</param>
		/// <returns></returns>
		string GetImagePath(Guid id);

		/// <summary>
		/// Save image
		/// </summary>
		/// <param name="parrentId">parrent id image</param>
		/// <param name="image">image</param>
		/// <returns></returns>
		Guid SaveImage(Guid parrentId, byte[] image);

		/// <summary>
		/// Delete image
		/// </summary>
		/// <param name="id">id image</param>
		/// <returns></returns>
		bool DeleteImage(Guid id);
	}
}
