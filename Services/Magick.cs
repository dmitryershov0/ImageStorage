using ImageMagick;

namespace ImageStorage.Services
{
	///<inheritdoc cref="IMagick"/>
	public class Magick : IMagick
	{
		///<inheritdoc cref="IMagick.MakeCircle(byte[])"/>
		public string MakeCircle(byte[] img)
		{
			using (var image = new MagickImage(img))
			{
				image.Format = MagickFormat.Png;
				image.Alpha(AlphaOption.Set);
				using (MagickImage copy = new MagickImage(image.Clone()))
				{
					copy.Distort(DistortMethod.DePolar, 0);
					copy.VirtualPixelMethod = VirtualPixelMethod.HorizontalTile;
					copy.BackgroundColor = MagickColors.None;
					copy.Distort(DistortMethod.Polar, 0);
					image.Compose = CompositeOperator.DstIn;
					image.Composite(copy, CompositeOperator.CopyAlpha);
				}
				return image.ToBase64();
			}
		}

		///<inheritdoc cref="IMagick.MakeResize(byte[], int, int)"/>
		public string MakeResize(byte[] img, int heigth, int width)
		{
			using (var image = new MagickImage(img))
			{
				image.Resize(width, heigth);
				return image.ToBase64();
			}
		}

		///<inheritdoc cref="IMagick.MakeTransperent(byte[])"/>
		public string MakeTransperent(byte[] img)
		{
			using (var image = new MagickImage(img))
			{
				image.Format = MagickFormat.Png;
				image.Alpha(AlphaOption.Set);
				image.ColorFuzz = new Percentage(10);
				image.Opaque(MagickColors.White, MagickColor.FromRgba(0, 0, 0, 0));
				return image.ToBase64();
			}
		}
	}
}
