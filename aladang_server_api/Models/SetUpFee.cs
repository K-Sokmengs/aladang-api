using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
    [Table("SetupFee_tbl")]
	public class SetUpFee
	{
        [Key]
        public int id { get; set; }
        public DateTime? date { get;set;}
        public string? feetype { get; set; }
        public decimal? amount { get; set; }
        public string? createby { get; set; }
        public DateTime? createdate { get; set; }
    }
}

