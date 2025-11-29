using System;
namespace aladang_server_api.ViewModel
{
	public class OrderDetailViewModel
	{
        public int fromshopid { get; set; }
        public int orderid { get; set; }
        public int productid { get; set; }
        public string? productcode { get; set; }
        public string? productname { get; set; }
        public int? qty { get; set; }
        public decimal? price { get; set; }
        public decimal? discount { get; set; }
        public int? invoiceno { get; set; }
        public DateTime? Date { get; set; }
        public int? shopId { get; set; }
        public string? shopName { get; set; }

        public int? customerId { get; set; }
        public string? customerName { get; set; }
        public DateTime registerdate { get; set; }
        public string? phone { get; set; }
        public string? tokenid { get; set; }
        public string? currentLocation { get; set; } 
        public string? gender { get; set; }
        public string? imageProfile { get; set; }
        public string? password { get; set; }

        public string? deliveryTypeIn { get; set; }   
        public string? paymentType { get; set; } 
        public string? bankName { get; set; }
        public string? accountNumber { get; set; }
        public string? accountName { get; set; }
        public string? receiptUpload { get; set; }
        public decimal? amountTobePaid { get; set; }
        public int? exchangeId { get; set; }
        public string? Status { get; set; }
    }
}

