namespace aladang_server_api.Models.BO.Req
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public string? DeliveryTypeIn { get; set; }
        public string? CurrentLocation { get; set; }
        public string? Phone { get; set; }
        public string? PaymentType { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? ReceiptUpload { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DeliveryAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public List<OrderShopRequest> OrderShops { get; set; }
    }

    public class OrderShopRequest
    {
        public int ShopId { get; set; }
        public string? QrcodeShopName { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }
    }

    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}