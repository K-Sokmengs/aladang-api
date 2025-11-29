using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("Reports")]
	public class Report
	{
		[Key]
		public int id { get; set; }
		public int? UserId { get; set; }
		public int? ReportTypeId { get; set; }
		public string? Objective { get; set; }
		public string? Via { get; set; }
		public string? Content { get; set; }
		public int? Durations { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? Location { get; set; }
		public string? Reference { get; set; }
	}
}

