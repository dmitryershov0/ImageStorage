namespace ImageStorage.Services
{
	/// <summary>
	/// Interface for works Magick.NET library
	/// </summary>
	public interface IMagick
	{
		/// <summary>
		/// Make circle image
		/// </summary>
		/// <param name="img">image</param>
		/// <returns></returns>
		string MakeCircle(byte[] img);

		/// <summary>
		/// Make resize image
		/// </summary>
		/// <param name="img">image</param>
		/// <param name="heigth">heigth</param>
		/// <param name="width">width</param>
		/// <returns></returns>
		string MakeResize(byte[] img, int heigth, int width);

		/// <summary>
		/// Сhange white background to transparent
		/// </summary>
		/// <param name="img">Image</param>
		/// <returns></returns>
		string MakeTransperent(byte[] img);
	}
}
