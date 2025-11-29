using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("currency_tbl")]
	public class Currency
	{
		[Key]
		public int id { get; set; }
		public string? currencyname { get; set; }
		public string? sign { get; set; }
		public string? status { get; set; }
	}
}

