using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aladang_server_api.Models
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		public int id { get; set; }
		public int FromShopId { get; set; }
        public int InvoiceNo { get; set; }
		public DateTime? Date { get; set; }
		public int ShopId { get; set; }
		public int CustomerId { get; set; }
		public string? DeliveryTypeIn { get; set; }
		public string? CurrentLocation { get; set; }
		public string? Phone { get; set; }
		public string? PaymentType { get; set; }
		public string? QrcodeShopName { get; set; }
		public string? BankName { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountName { get; set; }
		public string? ReceiptUpload { get; set; }
		public decimal AmountTobePaid { get; set; }
		public int ExchangeId { get; set; }
		public string? Status { get; set; }
		
		public decimal? DiscountAmount { get; set; }
		public decimal? DeliveryAmount { get; set; }
		public decimal? TaxAmount { get; set; }
	}
}

