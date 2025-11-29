using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
    [Table("ReportTypes")]
	public class ReportType
	{
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}

