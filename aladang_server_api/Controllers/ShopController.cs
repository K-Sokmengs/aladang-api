using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aladang_server_api.Configuration;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using aladang_server_api.Models.BO.Res;


namespace aladang_server_api.Controllers
{
    [Route("/app/v1/shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly AppDBContext _context;
        private ShopService _shopService;
        //private CustomerService customerService;

        public ShopController(ShopService shopService, AppDBContext appDbContext)
        {
            _shopService = shopService;
            _context = appDbContext;
        }

        [HttpGet, Route("all")]
        public IActionResult Get()
        { 
            List<Shop> result = _shopService.GetAll(); 
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _shopService.Count(),
                    countPage = _shopService.PageCount(),
                    currentPage = 1,
                    data = result 
                });
            }

            return NotFound(new
            {
                code = 404,
                message = "Data Not Found !",

            });
        }


        [HttpGet, Route("all/page")]
        public IActionResult GetShops(int page)
        { 
            List<Shop> result = _shopService.GetShops(page);  
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _shopService.Count(),
                    countPage = _shopService.PageCount(),
                    currentPage = page,
                    data = result

                });
            }

            return NotFound(new
            {
                code = 404,
                message = "Data Not Found !", 

            });
        }


        [HttpGet, Route("othershop")]
        public IActionResult GetOtherShop(int id,int page)
        {
            List<Shop> result = _shopService.GetOtherShop(id,page);
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _shopService.CountOtherShop(id),
                    countPage = _shopService.PageCountOtherShop(id),
                    currentPage = page,
                    data = result

                });
            }

            return NotFound(new
            {
                code = 404,
                message = "Data Not Found !",

            });
        }

        [HttpGet,Route("id")]
        public IActionResult GetById(int id)
        {
            var result = _shopService!.GetById(id);
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully", 
                    pageCount = 1,
                    currentPage = 1,
                    data = result

                });
            }

            return NotFound(new
            {
                code = 404,
                message = "Data Not Found !", 

            });
        }


        [HttpGet,Route("status/page")]
        public IActionResult GetByStatus(string status, int page)
        {
            List<Shop> result = _shopService!.GetByStatus(status,page);  
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _shopService.Count(status),
                    countPage = _shopService.PageCount(status),
                    currentPage = page,
                    data = result

                });
            }

            return NotFound(new
            {
                code = 404,
                message = "Data Not Found !",

            });
        }

        // POST api/values
        [HttpPost("create")]
        public ActionResult Post([FromBody] Shop req)
        {
            var result=_shopService.CreateNew(req);
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = 1,
                    countPage = 1,
                    currentPage = 1,
                    data = result

                });
            }
            return BadRequest();
        }

        [HttpPut("update")]
        public ActionResult Put([FromBody] Shop req)
        {
            var result =_shopService.Update(req);
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = 1,
                    countPage = 1,
                    currentPage = 1,
                    data = result

                });
            }
            return BadRequest();
        }

        [HttpPut("changepassword")]
        public ActionResult ChangePassword([FromBody] ChangePasswordShop req)
        {
            var result = _shopService.changePasswordShop(req);
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = 1,
                    countPage = 1,
                    currentPage = 1,
                    data = result

                });
            }
            return BadRequest();
        }



        [HttpPut("resetpassword")]
        public ActionResult ResetPassword([FromBody] ResetPasswordShop req)
        {
            var result = _shopService.resetPasswordShop(req);

            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = 1,
                countPage = 1,
                currentPage = 1,
                data = result

            });
        }

        [HttpPut("change-logo-shop")]
        public ActionResult changeLogoShop(ChangeLogoShop req)
        {
            var result = _shopService.changeLogoShop(req);

            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = 1,
                countPage = 1,
                currentPage = 1,
                data = result

            });
        }
        [HttpPut("change-logo-qr")]
        public ActionResult changeLogoQR(ChangeLogoQRCode req)
        {
            var result = _shopService.changeLogoQRCodeImage(req);

            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = 1,
                countPage = 1,
                currentPage = 1,
                data = result

            });
        }
         [HttpPost("deactivate")]
        public IActionResult Deactivate(DeactivateRequest req)
        {
            
            if (req.UserType == "customer")
            {
                var customer = _context.customers.SingleOrDefault(c => c.id == req.Id);
                if (customer == null) return NotFound();
                customer.status = "DEL";
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else if (req.UserType == "shop")
            {
                var shop = _context.shops.SingleOrDefault(c => c.id == req.Id);
                if (shop == null) return NotFound();
                shop.status = "InActive";
                _context.Entry(shop).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                data = ""
            });
        }
    }
    
}

