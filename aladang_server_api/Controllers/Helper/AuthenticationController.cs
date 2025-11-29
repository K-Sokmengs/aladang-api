using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using aladang_server_api.Helpers;
using aladang_server_api.Models.AppAuthorize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace aladang_server_api.Controllers.Helper
{
    [Route("/app/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateRequest user)
        {

            if (user is null)
            {
                return BadRequest("Invalid user request!!!");
            }
            if (user.Username == "adminApi2023" && user.Password == "Pass@777s;ldjfdjwyew(76676655")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerApp.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManagerApp.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManagerApp.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddDays(6), signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse
                {
                    Token = tokenString
                });
            }
            return Unauthorized();
        }
    }
}
