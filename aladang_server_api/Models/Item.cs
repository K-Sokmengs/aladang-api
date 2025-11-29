using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("tbl_item")]
	public class Item
	{
		[Key]
		public int id { get; set; }
		public string? name { get; set; }
	}
}

