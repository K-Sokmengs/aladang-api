using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using aladang_server_api.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aladang_server_api.Configuration;
using aladang_server_api.Models.AppAuthorize;
using aladang_server_api.Models.BO.Res;

namespace aladang_server_api.Services
{
    public interface IUserServiceWithToken
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<AppUserAuthorizeRes> GetAll();
        AppUserAuthorizeRes GetById(int id);
    }

    public class UserServicewithToken : IUserServiceWithToken
    { 
        private List<AppUserAuthorizeRes> _users = new List<AppUserAuthorizeRes>
        {
            new AppUserAuthorizeRes { id = 0,  userName = "Sambacappapi", password = "KpGre#%huyTROubv67878jhJfafdsfa",email="admin@local", userStatus=true}
        };
         
        private AppDBContext _contex;
         
        public UserServicewithToken( AppDBContext dBContex)
        { 
            _contex = dBContex;
            var usLogin = _contex.appUserAuthorizes!.Where(x =>  x.userStatus == true);
            if (usLogin != null)
            {
                foreach (var usr in usLogin)
                {
                    var ustem = new AppUserAuthorizeRes { id = usr.id, userName = usr.userName, password = usr.password,email=usr.email ,userStatus = usr.userStatus };
                    _users.Add(ustem);
                }
            } 
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.FirstOrDefault(x => x.userName == model.Username && x.password == model.Password && x.userStatus == true);
            if (user == null)
            { 
                return null!; 
            }
             
            var token = generateJwtToken(user); 
            return new AuthenticateResponse(user, token); 
        }

        public IEnumerable<AppUserAuthorizeRes> GetAll()
        {
            return _users;
        }

        public AppUserAuthorizeRes GetById(int id)
        {
            return _users.FirstOrDefault(x => x.id == id)!;
        }
 
        private string generateJwtToken(AppUserAuthorizeRes user)
        {           
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerApp.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManagerApp.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManagerApp.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddDays(6), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}

