namespace aladang_server_api.Models.BO.Req
{
	public class ProductReq
	{
		public int Id { get; set; }
		public int? ShopId { get; set; }
		public string? ProductCode { get; set; }
		public string? ProductName { get; set; }
		public string? Description { get; set; }
		public int? QtyInStock { get; set; }
		public decimal? Price { get; set; }
		public decimal? NewPrice { get; set; }
        public int? CurrencyId { get; set; }
		public string? CutStockType { get; set; }
		public DateTime? ExpiredDate { get; set; }
		public string? LinkVideo { get; set; }
		public string? ImageThumbnail { get; set; }
		public string? Status { get; set; }
		public List<ProductImageDetailReq>? ProductImageDetails { get; set; } 
	}
}

