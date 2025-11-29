using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aladang_server_api.Migrations;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/order")]
    [ApiController, Authorize]
    public class OrderController : ControllerBase
    {
        private IOrder appService;
        private readonly AppDBContext _context;

        public OrderController(IOrder orderService, AppDBContext appDBContext)
        {
            this.appService = orderService;
            this._context = appDBContext;
        }

        [HttpPost("post-order")]
        public async Task<ActionResult<Order>> PostOrder(Order req)
        {
            _context.orders!.Add(req);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = req.id }, req);
        }



        [HttpGet("all")]
        public IActionResult Get()
        {
            List<Order> result = appService.GetAll();
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.Count(),
                countPage = 1,
                currentPage = 1,
                data = result

            });
        }

        [HttpGet("all/page")]
        public IActionResult Get(int page)
        {
            List<Order> result = appService.GetOrder(page);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.Count(),
                countPage = appService.PageCount(),
                currentPage = page,
                data = result

            });
        }

        [HttpGet("shopid/page")]
        public IActionResult GetByShop(int shopid, int page, string status)
        {
            List<Order> result = appService.GetByShopId(shopid, page, status);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.CountS(shopid, status),
                countPage = appService.PageCountS(shopid, status),
                currentPage = page,
                data = result

            });
        }

        [HttpGet("shopid")]
        public IActionResult GetByShop(int shopid)
        {
            List<Order> result = appService.GetByShopId(shopid);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.CountS(shopid),
                countPage = 1,
                currentPage = 1,
                data = result

            });
        }

        [HttpGet("customer-by-shop/{shopid}")]
        public IActionResult GetByCustomer(int shopid)
        {
            List<Order> result = appService.GetByCustomer(shopid);
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

        [HttpGet("customerid/page")]
        public IActionResult GetByCustomer(int customerid, int page, string status)
        {
            List<Order> result = appService.GetByCustomerId(customerid, page, status);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.CountC(customerid, status),
                countPage = appService.PageCountC(customerid, status),
                currentPage = page,
                data = result

            });
        }

        [HttpGet("status/page")]
        public IActionResult GetByStatus(string status, int page)
        {
            List<Order> result = appService!.GetByStatus(status, page);
            double pagecount = appService.PageCount(status);
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = appService.Count(status),
                    countPage = appService.PageCount(status),
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


        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var result = appService!.GetById(id);
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

            return NotFound(new
            {
                code = 404,
                message = "Data Not Found !",
                data = result

            });
        }


        // POST api/values
        /*  [HttpPost("create")]
          public ActionResult Post([FromBody] Order req)
          {
              var result = appService.CreateNew(req);

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

              return NotFound(new
              {
                  code = 404,
                  message = "Data Not Found !",
                  data = result

              });
          }
  */

        [HttpPost("create")]
        public ActionResult Post([FromBody] OrderMultiReq req)
        {
            var result = appService.CreateNew(req);

            if (result != null && result.Count > 0)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = result.Count,
                    data = result
                });
            }

            return NotFound(new
            {
                code = 404,
                message = "Data Not Found !"
            });
        }

        [HttpPut("update")]
        public ActionResult Put([FromBody] Order req)
        {
            var result = appService.Update(req);
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

        [HttpPost("tracking/update")]
        public ActionResult TrackingUpdate([FromBody] OrderTrackingRequest req)
        {
            var result = appService.UpdateTracking(req);
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
        
        [HttpGet("tracking/getByOrderId/{id:int}")]
        public IActionResult GetTrackingByOrderId(int id)
        {
            var result = appService.GetOrderTrackingByOrderId(id);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.Count(),
                countPage = 1,
                currentPage = 1,
                data = result

            });
        }
    }
}

