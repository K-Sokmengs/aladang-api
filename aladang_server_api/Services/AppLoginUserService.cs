using System;
using Microsoft.IdentityModel.Tokens;
using aladang_server_api.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models.AppAuthorize;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using aladang_server_api.Migrations;

namespace aladang_server_api.Services
{
	public class AppLoginUserService : IAppLoginUser
    {
        private readonly IConfiguration _configuration; 
        private AppDBContext _contex;

        public AppLoginUserService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration; 
        }


        public AppLoginAuthorizeRes appCustomerLogin(AppUserLoginReq req)
        {
            AppLoginAuthorizeRes appLoginAuthorizeRes = new AppLoginAuthorizeRes();

            var phonereq = req.phone!.Replace(" ", "").ToLower();
            //var pwdHas = Encrypt.EncriptSha256PassWord(req.password!);

            var result = _contex.customers!.FirstOrDefault(c => c.phone!.ToLower().Replace(" ", "") == phonereq && c.password == req.password && c.status=="ACT");
            if (result != null)
            {
                appLoginAuthorizeRes.id = result.id;
                appLoginAuthorizeRes.userName = result.customerName;
                appLoginAuthorizeRes.phone = result.phone;
                appLoginAuthorizeRes.password = result.password;
                appLoginAuthorizeRes.imageProfile = result.imageProfile;
                appLoginAuthorizeRes.token = generateJwtToken(req);
                appLoginAuthorizeRes.usertype = req.usertype;
                appLoginAuthorizeRes.ostype = req.ostype;
                appLoginAuthorizeRes.tokenid = req.tokenid;
                appLoginAuthorizeRes.expireddate = DateTime.Now.AddDays(6);
                return appLoginAuthorizeRes;
            }
            else
            {
                return null!;
            }
        }
        public AppLoginAuthorizeRes appShopLogin(AppUserLoginReq req)
        {
            AppLoginAuthorizeRes appLoginAuthorizeRes = new AppLoginAuthorizeRes();

            var phonereq = req.phone!.Replace(" ", "").ToLower();
            //var pwdHas = Encrypt.EncriptSha256PassWord(req.password!);

            var result = _contex.shops!.FirstOrDefault(c => c.phone!.ToLower().Replace(" ", "") == phonereq && c.password == req.password
            && c.status=="Active"
            );
          
                appLoginAuthorizeRes.id = result.id;
                appLoginAuthorizeRes.userName = result.ownerName;
                appLoginAuthorizeRes.phone = result.phone;
                appLoginAuthorizeRes.password = result.password;
                appLoginAuthorizeRes.token = generateJwtToken(req);
                appLoginAuthorizeRes.usertype = req.usertype;
                appLoginAuthorizeRes.ostype = req.ostype;
                appLoginAuthorizeRes.tokenid = req.tokenid;
                appLoginAuthorizeRes.expireddate = DateTime.Now.AddDays(6);
                return appLoginAuthorizeRes;
         
        }


        private string generateJwtToken(AppUserLoginReq req)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerApp.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManagerApp.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManagerApp.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddDays(6), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}

