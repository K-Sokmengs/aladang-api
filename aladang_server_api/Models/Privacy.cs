using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
    [Table("privacy_tbl")]
	public class Privacy
    {
        [Key]
        public int id { get; set; }
        public string? description { get; set; }
        public string? descriptionenglish { get; set; }
    }
}

