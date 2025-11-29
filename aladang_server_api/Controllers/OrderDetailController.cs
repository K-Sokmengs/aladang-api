using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using aladang_server_api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using aladang_server_api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/orderdetail")]
    [ApiController, Authorize]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetail appService;

        public OrderDetailController(IOrderDetail orderDetailService)
        {
            this.appService = orderDetailService;
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            List<OrderDetail> result = appService.GetAll(); 
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
            List<OrderDetail> result = appService.GetOrderDetail(page); 
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



        [HttpGet("orderid")]
        public IActionResult GetByOrderId(int orderid)
        {
            List<OrderDetail> result = appService.GetByOrderId(orderid); 
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.CountO(orderid),
                countPage = appService.PageCountO(orderid),
                currentPage = 1,
                data = result

            });
        }


        [HttpGet("orderdetailview/orderid")]
        public IActionResult GetOrderDetailViewModelByOrderId(int orderid)
        {
            List<OrderDetailViewModel> result = appService.GetOrderDetailViewModel(orderid);
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


        [HttpGet("productid/page")]
        public IActionResult GetByProductId(int productid, int page)
        {
            List<OrderDetail> result = appService.GetByProductId(productid,page);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.CountP(productid),
                countPage = appService.PageCountP(productid),
                currentPage = page,
                data = result

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
            });
        }

        // POST api/values
        [HttpPost("create")]
        public ActionResult Post([FromBody] OrderDetail req)
        {
            var result=appService.CreateNew(req);

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

        [HttpPut("update")]
        public ActionResult Put([FromBody] OrderDetail req)
        {
            var result=appService.Update(req);

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
    }
}

