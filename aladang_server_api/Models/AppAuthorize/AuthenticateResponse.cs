using System;
using aladang_server_api.Models.BO.Res;

namespace aladang_server_api.Models.AppAuthorize
{
	public class AuthenticateResponse
	{
        private AppUserAuthorizeRes? appUserAuthorizeRes;
        private object? token;

        public int id { get; set; }
        public string? userName { get; set; }
        public string? email { get; set; }
        public string? Token { get; set; }
        public DateTime createDate { get; set; }
        public DateTime expiredDate { get; set; }


        public AuthenticateResponse(AppUserAuthorizeRes appUserAuthRes, string _token)
        {
            id = appUserAuthRes.id;
            email = appUserAuthRes.email;
            userName = appUserAuthRes.userName;
            Token = _token;
            createDate = DateTime.Now;
            expiredDate = DateTime.Now.AddDays(6);
        }

        public AuthenticateResponse(AppUserAuthorizeRes _appUserAuthRes, object token)
        {
            this.appUserAuthorizeRes = _appUserAuthRes;
            this.token = token;
        }
    }
}

