using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("banner_tbl")]
	public class Banner
	{
		[Key]
		public int id { get; set; }
		public DateTime date { get; set; }
		public string? userid { get; set; }
		public int shopid { get; set; }
		public DateTime exireddate { get; set; }
		public int qtymonth { get; set; }
		public string? bannerimage { get; set; }
		public string? bannerstatus { get; set; }
	}
}

