using System;
namespace aladang_server_api.Models.BO.Req
{
	public class AppUserLoginReq
	{
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? usertype { get; set; }
        public string? ostype { get; set; }
        public string? tokenid { get; set; }
    }
}

