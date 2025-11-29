namespace aladang_server_api.Models.BO.Req
{
    public class OrderMultiReq
    {
        public List<int> FromShopIds { get; set; } = new List<int>();
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
    }

}
