using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStorage.Helpers
{
	public static class Base64StringHelper
	{
		public static byte[] GetBytesArray(this string image)
		{
			return Convert.FromBase64String(image);
		}

		public static string GetBase64String(this byte[] imgByte)
		{
			return Convert.ToBase64String(imgByte, 0, imgByte.Length);
		}
	}
}
