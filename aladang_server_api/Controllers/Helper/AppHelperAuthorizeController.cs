using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aladang_server_api.Models.AppAuthorize;
using aladang_server_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aladang_server_api.Controllers.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppHelperAuthorizeController : ControllerBase
    {
        private IUserServiceWithToken _userService;

        public AppHelperAuthorizeController(IUserServiceWithToken userService)
        {
            _userService = userService;
        }

        [HttpPost(Name ="authenticate"),AllowAnonymous]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
            {
                //return BadRequest(new { message = "Username or password is incorrect" });
                return BadRequest(new
                {
                    code = 400,
                    message = "Username or password is incorrect",
                    data = response
                });
            }


            //return Ok(response);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                data = response

            });
        }

        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
