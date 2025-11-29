using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
    [Table("tbl_DeliveryType")]
    public class DeliveryType
	{
		[Key]
		public int id { get; set; }
		public string? delivery_name { get; set; }
		public string? status { get; set; }
		public decimal? fee { get; set; }
    }
}

	