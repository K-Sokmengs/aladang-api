using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("exchange_tbl")]
	public class Exchange
	{
		[Key]
		public int id { get; set; }
		public DateTime date { get; set; }
		public int currencyid { get; set; }
		public int shopid { get; set; }
		public decimal rate { get; set; }
	}
}

