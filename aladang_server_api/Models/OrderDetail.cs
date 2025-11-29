using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("orderdetail_tbl")]
	public class OrderDetail
	{
		[Key]
		public int id { get; set; }
		public int orderid { get; set; } 
        public int productid { get; set; }
		public int qty { get; set; }
		public decimal price { get; set; }
		public decimal discount { get; set; }
	}
}

