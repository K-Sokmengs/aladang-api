using System;
using aladang_server_api.Interface;
using aladang_server_api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aladang_server_api.Models;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/report")]
    [ApiController, Authorize]
    public class ReportController : ControllerBase
    {
        private IReport appService;

        public ReportController(IReport appService)
        {
            this.appService = appService;
        }
        [HttpGet("get-order-detail")]
        public IActionResult Get()
        {
            List<OrderDetailViewModel> result = appService.GetOrderDetail();
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",  
                data = result

            });
        }

        [HttpGet("get-order-detail-by-shopid")]
        public IActionResult Get(int id)
        {
            List<OrderDetailViewModel> result = appService.GetOrderDetailByShopId(id);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                data = result

            });
        }

        // [HttpGet("all/page")]
        // Public IActionResult Get(int page)
        // {
        //     List<Report> result = appService.GetReports(page);
        //     return Ok(new
        //     {
        //         code = 200,
        //         message = "Data Successfully",
        //         count = appService.Count(),
        //         countPage = appService.PageCount(),
        //         currentPage = page,
        //         data = result

        //     });
        // }


        // [HttpGet("id")]
        // Public IActionResult GetById(int id)
        // {
        //     var result = appService!.GetReportById(id);
        //     if (result != null)
        //     {
        //         return Ok(new
        //         {
        //             code = 200,
        //             message = "Data Successfully",
        //             count = 1,
        //             countPage = 1,
        //             currentPage = 1,
        //             data = result

        //         });
        //     }

        //     return NotFound(new
        //     {
        //         code = 404,
        //         message = "Data Not Found !",
        //         data = result

        //     });
        // }

        // // POST api/values
        // [HttpPost("create")]
        // Public ActionResult Post([FromBody] Report req)
        // {
        //     var result = appService.CreateNew(req);
        //     return Ok(new
        //     {
        //         code = 200,
        //         message = "Data Successfully",
        //         count = 1,
        //         countPage = 1,
        //         currentPage = 1,
        //         data = result

        //     });
        // }

        // [HttpPut("update")]
        // Public ActionResult Put([FromBody] Report req)
        // {
        //     var result = appService.Update(req);
        //     return Ok(new
        //     {
        //         code = 200,
        //         message = "Data Successfully",
        //         count = 1,
        //         countPage = 1,
        //         currentPage = 1,
        //         data = result

        //     });
        // }
    }
}

