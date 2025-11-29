using System;
namespace aladang_server_api.Models.BO.Req
{
	public class CustomerReq
	{
        public int id { get; set; }
        public DateTime date { get; set; }
        public string? phone { get; set; }
        public string? tokenid { get; set; }
        public string? currentLocation { get; set; }
        public string? customerName { get; set; }
        public string? gender { get; set; }
        public string? imageProfile { get; set; }
        public string? password { get; set; }
        public string? status { get; set; }
    }
}

