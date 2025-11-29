using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using aladang_server_api.Models;
using aladang_server_api.Models.AppAuthorize;

namespace aladang_server_api.Services
{
	public interface AppUserLoginService
	{
        public AppUserLoginRes appUserLoginCustomer(AppUserLoginReq req);
        public AppUserLoginRes appUserLoginShop(AppUserLoginReq req);
    }

    public class AppUserLoginServiceImpl: AppUserLoginService
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "";
        private string appDomain = "";
        private AppDBContext _contex;

        public AppUserLoginServiceImpl(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
             appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;  
        }


        public AppUserLoginRes appUserLoginCustomer(AppUserLoginReq req)
        {
            var phonereq1 = req.phone!.Replace(" ", "");
            var phonereq2 = phonereq1.ToLower();
            //var pwdHas = Encrypt.EncriptSha256PassWord(req.password!);

            AppUserLoginRes appUserLoginRes = new AppUserLoginRes();
            var result = _contex.customers!.FirstOrDefault(c => c.phone!.ToLower().Replace(" ", "") == phonereq2 && c.password == req.password && c.status=="ACT");
            if (result != null)
            {
                result!.imageProfile = String.IsNullOrEmpty(result.imageProfile) ? defaultUrlNoImg : basicUrlImg + result.imageProfile;
                appUserLoginRes.setCustomerUser(result);
                return appUserLoginRes;
            }
            else
            {
                return null!;
            }
        }

        public AppUserLoginRes appUserLoginShop(AppUserLoginReq req)
        {
            var phonereq1 = req.phone!.Replace(" ", "");
            var phonereq2 = phonereq1.ToLower();
            //var pwdHas = Encrypt.EncriptSha256PassWord(req.password!);
            AppUserLoginRes appUserLoginRes = new AppUserLoginRes();

            var result = _contex.shops!.FirstOrDefault(c => c.phone!.ToLower().Replace(" ", "") == phonereq2 && c.password == req.password && c.status=="Active");
            if (result != null)
            {
                result!.logoShop = String.IsNullOrEmpty(result.logoShop) ? defaultUrlNoImg : basicUrlImg + result.logoShop;
                result!.qrCodeImage = String.IsNullOrEmpty(result.qrCodeImage) ? defaultUrlNoImg : basicUrlImg + result.qrCodeImage;
                appUserLoginRes.setShopUser(result);
                return appUserLoginRes;
            }
            else
            {
                return null!;
            }
        }
    }
}

