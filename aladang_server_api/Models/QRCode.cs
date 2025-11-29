using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
    [Table("Qrcode_tbl")]
	public class QRCode
	{
        [Key]
        public int id { get; set; }
        public string? qrcode { get; set; }
        public string? createby { get; set; }
        public DateTime? createdate { get; set; }
    }
}

