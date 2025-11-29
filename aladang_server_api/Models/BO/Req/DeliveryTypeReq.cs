using System;
namespace aladang_server_api.Models.BO.Req
{
	public class DeliveryTypeReq
	{
        public int id { get; set; }
        public string? delivery_name { get; set; }
        public string? status { get; set; }
    }
}

