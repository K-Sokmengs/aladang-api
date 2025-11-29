//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using aladang_server_api.Models.BO.Req;
//using aladang_server_api.Models.BO.Res;
//using aladang_server_api.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace aladang_server_api.Controllers
//{
//    [Route("/app/v1/AppUserLogin")]
//    [ApiController, Authorize]
//    public class AppUserLoginController : ControllerBase
//    {
//        private AppUserLoginService appService;

//        public AppUserLoginController(AppUserLoginService _appService)
//        {
//            this.appService = _appService;
//        } 

//        [HttpPost, AllowAnonymous]
//        public IActionResult AuthenticateLogin(AppUserLoginReq req)
//        {
//            AppUserLoginRes appUserLoginRes = new AppUserLoginRes();
//            if (req.usertype == "customer")
//            {
//                appUserLoginRes = appService.appUserLoginCustomer(req);
//            }
//            else
//            { 
//                appUserLoginRes = appService.appUserLoginShop(req);
//            }

//            if (appUserLoginRes == null)
//            {
//                return BadRequest(new
//                {
//                    code = 400,
//                    message = "Username or password is incorrect",
//                    data = appUserLoginRes
//                });
//            }

//            return Ok(new
//            {
//                code = 200,
//                message = "Data Successfully",
//                data = appUserLoginRes 
//            });
//        } 
//    }
//}
