using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
    [Table("pamentmethod_tbl")]
	public class PaymentMethod
	{
        [Key]
        public int id { get; set; }
        public string? methodname { get; set; }
        public string? createby { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
    }
}

