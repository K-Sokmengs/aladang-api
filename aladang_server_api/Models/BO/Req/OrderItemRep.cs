namespace aladang_server_api.Models.BO.Req
{
    public class OrderItemReq
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class ShopOrderReq
    {
        public int ShopId { get; set; }
        public List<OrderItemReq> Items { get; set; } = new();
    }

    public class CreateOrderReq
    {
        public int CustomerId { get; set; }
        public string? DeliveryType { get; set; }
        public List<ShopOrderReq> ShopOrders { get; set; } = new();
    }

}
