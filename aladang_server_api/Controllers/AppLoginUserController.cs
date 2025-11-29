using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aladang_server_api.Interface;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aladang_server_api.Services;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/AppLoginUser")]
    [ApiController]
    public class AppLoginUserController : ControllerBase
    {
        private IAppLoginUser appLoginUser;

        public AppLoginUserController(IAppLoginUser appLoginUser)
        {
            this.appLoginUser = appLoginUser;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult AppLoginUser(AppUserLoginReq req)
        {
            AppLoginAuthorizeRes appUserLoginRes = new AppLoginAuthorizeRes();
            if (req == null)
            {
                return BadRequest();
            }
            if (req.usertype == "customer")
            {
                appUserLoginRes = appLoginUser.appCustomerLogin(req);
            }
            else if (req.usertype == "shop")
            { 
                appUserLoginRes = appLoginUser.appShopLogin(req);
            }

            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                data = appUserLoginRes
            });
        }

        //[HttpPost("ShopLogin"), AllowAnonymous]
        //Public IActionResult ShopLogin(AppUserLoginReq req)
        //{
        //    ShopRes shopRes = new ShopRes();

        //    shopRes = appService.UserLoginShop(req);
        //    if (shopRes == null)
        //    {
        //        return BadRequest(new
        //        {
        //            code = 400,
        //            message = "Username or password is incorrect",
        //            data = shopRes
        //        });
        //    }

        //    return Ok(new
        //    {
        //        code = 200,
        //        message = "Data Successfully",
        //        data = shopRes

        //    });
        //}

        //[HttpPost("CustomerLogin"), AllowAnonymous]
        //Public IActionResult CustomerLogin(AppUserLoginReq req)
        //{
        //    CustomerRes customerRes = new CustomerRes();

        //    customerRes = appService.UserLoginCustomer(req);
        //    if (customerRes == null)
        //    {
        //        return BadRequest(new
        //        {
        //            code = 400,
        //            message = "Username or password is incorrect",
        //            data = customerRes
        //        });
        //    }

        //    return Ok(new
        //    {
        //        code = 200,
        //        message = "Data Successfully",
        //        data = customerRes

        //    });
        //}


        //[HttpPost, AllowAnonymous]
        //Public IActionResult UserLogin(AppUserLoginReq req)
        //{
        //    ShopRes shopRes = new ShopRes();
        //    CustomerRes customerRes = new CustomerRes();

        //    if (req.usertype == "customer")
        //    {
        //        customerRes = appService.UserLoginCustomer(req);
        //        return Ok(new
        //        {
        //            code = 200,
        //            message = "Data Successfully",
        //            data = customerRes

        //        });
        //    }
        //    else if (req.usertype == "shop")
        //    {
        //        shopRes = appService.UserLoginShop(req);
        //        return Ok(new
        //        {
        //            code = 200,
        //            message = "Data Successfully",
        //            data = shopRes
        //        });
        //    }
        //    return BadRequest(new
        //    {
        //        code = 400,
        //        message = "Username or password is incorrect",
        //        data = customerRes
        //    });


        //}
    }
}

