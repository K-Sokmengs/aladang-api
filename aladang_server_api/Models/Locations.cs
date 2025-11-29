using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("Location_tbl")]
	public class Locations
	{
		[Key]
		public int id { get; set; }
		public string? location { get; set; }
		public string? active { get; set; }
	}
}

