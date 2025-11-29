using System;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aladang_server_api.Models.BO.Res;
using aladang_server_api.Services;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/deliverytype")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private IDeliveryType appService;

        public DeliveryTypeController(IDeliveryType _appService)
        {
            this.appService = _appService;
        }
        [HttpGet("all")]
        public IActionResult Get()
        {
            List<DeliveryType> result = appService.GetAll();  
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

        [HttpGet("status/page")]
        public IActionResult Get(string status,int page)
        {
            List<DeliveryType> result = appService.GetByStatus(status,page);
            double pagecount = appService.PageCount(status); 
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.Count(status),
                countPage = appService.PageCount(status),
                currentPage = 1,
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
        public ActionResult Post([FromBody] DeliveryType req)
        {
            var resulst=appService.CreateNew(req);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = 1,
                countPage = 1,
                currentPage = 1,
                data = resulst
            });
        }

        [HttpPut("update")]
        public ActionResult Put([FromBody] DeliveryType req)
        {
            var result = appService.Update(req);

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

