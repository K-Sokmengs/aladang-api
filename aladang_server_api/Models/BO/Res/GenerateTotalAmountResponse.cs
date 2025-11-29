namespace aladang_server_api.Models.BO.Req;

public class GenerateTotalAmountResponse
{
    public decimal? ExchangeRate { get; set; }
    public string? ExchangeRateAmount { get; set; } // $1=៛4100
    public string? Currency { get; set; }
    public decimal? TotalAmount { get; set; } // ៛១០០០០០
    public string? TotalAmountFormat { get; set; }
}