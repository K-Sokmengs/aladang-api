namespace aladang_server_api.Models.BO.Req;

public class GenerateTotalAmountRequest
{
    public int? ShopId { get; set; }
    public int? CurrencyId { get; set; }
    public decimal? TotalAmount { get; set; }
}