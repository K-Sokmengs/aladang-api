using System;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;

namespace aladang_server_api.Interface
{
	public interface IAppLoginUser
	{
        public AppLoginAuthorizeRes appCustomerLogin(AppUserLoginReq req);
        public AppLoginAuthorizeRes appShopLogin(AppUserLoginReq req);
    }
}

