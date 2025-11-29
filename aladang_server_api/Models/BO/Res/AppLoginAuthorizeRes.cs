using System;
namespace aladang_server_api.Models.BO.Res
{
	public class AppLoginAuthorizeRes
	{
        public int id { get; set; }
        public string? userName { get; set; }
        public string? phone { get; set; }
        public string? imageProfile { get; set; }
        public string? password { get; set; }
        public string? token { get; set; }
        public string? usertype { get; set; }
        public string? ostype { get; set; }
        public string? tokenid { get; set; }
        public DateTime expireddate { get; set; } 
    }
}

