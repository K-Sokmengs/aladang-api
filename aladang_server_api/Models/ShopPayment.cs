using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("shopPayment_tbl")]
	public class ShopPayment
	{
		[Key]
		public int id { get; set; }
		public DateTime? date { get; set; }
		public int? shopid { get; set; }
		public string? paytype { get; set; }
		public DateTime? startdate { get; set; }
		public DateTime? enddate { get; set; }
		public decimal? amount { get; set; }
		public string? note { get; set; }
	}
}

