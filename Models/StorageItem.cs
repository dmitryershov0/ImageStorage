using System;

namespace ImageStorage.Models
{
	/// <summary>
	/// Item redis storage
	/// </summary>
	public class StorageItem
	{
		/// <summary>
		/// Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Parrent Id default Guid.Empty
		/// </summary>
		public Guid ParrentId { get; set; } = Guid.Empty;

	}
}
