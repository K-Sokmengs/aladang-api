namespace aladang_server_api.Models.BO.Req;

public class OrderTrackingRequest
{
    public int Id { get; set; }
    public int? DeliveryTrackingTypeId { get; set; }
    public int? OrderId { get; set; }
    public string? Status { get; set; }
    public string? Remark { get; set; }
    public int ShopId { get; set; }
    public object UserId { get; internal set; }
}