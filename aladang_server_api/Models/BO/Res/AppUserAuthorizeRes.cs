using System;
namespace aladang_server_api.Models.BO.Res
{
	public class AppUserAuthorizeRes
	{
        public int id { get; set; }
        public string? userName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; } 
        public DateTime expireddate { get; set; }
        public bool userStatus { get; set; }
        public string? createby { get; set; }
        public DateTime createdate { get; set; }
        public DateTime updatedate { get; set; }
    }
}

