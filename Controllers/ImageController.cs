using System;
using ImageStorage.Helpers;
using ImageStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageStorage.Controllers
{
	/// <summary>
	/// Image storage contoller
	/// </summary>
	[ApiController]
	[Route("api/[controller]/v1")]
	public class ImageController : ControllerBase
	{
		private readonly IMagick _magickService;
		private readonly IStorage _storageService;

		/// <summary>
		/// DI frough constructor
		/// </summary>
		/// <param name="magickService">Magick service</param>
		/// <param name="storageService">Storage service</param>
		public ImageController(IMagick magickService, IStorage storageService)
		{
			_magickService = magickService;
			_storageService = storageService;
		}

		/// <summary>
		/// Add image
		/// <remarks>
		/// Sample request:
		///
		///     POST imgage Base64String from body
		///
		/// </remarks>
		/// </summary>
		/// <param name="parrentId">Previos image id</param>
		/// <param name="img">image</param>
		/// <returns>Id image</returns>
		[HttpPost("add/{parrentId}")]
		[HttpPost("add")]
		public ActionResult<Guid> Add([FromBody]string img,
			Guid? parrentId = null)
		{
			var key = _storageService
				.SaveImage(parrentId.GetValueOrDefault(), img.GetBytesArray());
			return Ok(key);
		}

		/// <summary>
		/// Get image 
		/// </summary>
		/// <param name="id">image id</param>
		[HttpGet("get/{id}")]
		[HttpGet("get")]
		public ActionResult<string> Get(Guid? id = null)
		{
			var imgByte = _storageService.GetImage(id.GetValueOrDefault());
			return imgByte.GetBase64String();
		}

		/// <summary>
		/// Get previos image 
		/// </summary>
		/// <param name="id">image id</param>
		[HttpGet("getprevios/{id}")]
		public ActionResult<string> GetPrevios(Guid id)
		{
			var imgByte = _storageService.GetPreviosImage(id);
			return imgByte.GetBase64String();
		}

		/// <summary>
		/// Get circle image
		/// </summary>
		/// <param name="id">image id</param>
		[HttpGet("getcircle/{id}")]
		[HttpGet("getcircle")]
		public ActionResult<string> GetCircle(Guid? id = null)
		{
			var img = _storageService.GetImage(id.GetValueOrDefault());
			return _magickService.MakeCircle(img);
		}

		/// <summary>
		/// Get resize image
		/// </summary>
		/// <param name="id">image id</param>
		/// <param name="height">height</param>
		/// <param name="width">width</param>
		[HttpGet("getresize/{height}/{width}/{id}")]
		[HttpGet("getresize/{height}/{width}")]
		public ActionResult<string> GetResize(int height, int width, Guid? id = null)
		{
			var img = _storageService.GetImage(id.GetValueOrDefault());
			return _magickService.MakeResize(img, height, width);
		}

		/// <summary>
		/// Delete image
		/// </summary>
		/// <param name="id">Id image</param>
		/// <response code="201">Returns true</response>
		/// <response code="400">Return false if try delete default image</response>  
		[HttpDelete("delete/{id}")]
		public ActionResult<bool> Delete(Guid id)
		{
			if (id.Equals(Guid.Empty))
				return BadRequest(false);
			return Ok(_storageService.DeleteImage(id));
		}
	}
}
