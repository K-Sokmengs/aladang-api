using System;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/paymentmethod")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private IPaymentMethod appService;

        public PaymentMethodController(IPaymentMethod appService)
        {
            this.appService = appService;
        }
        [HttpGet("all")]
        public IActionResult Get()
        {
            List<PaymentMethod> result = appService.GetAll();
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.Count(),
                countPage = appService.PageCount(),
                currentPage = 1,
                data = result

            });
        }

        [HttpGet("all/page")]
        public IActionResult Get(int page)
        {
            List<PaymentMethod> result = appService.GetPaymentMethods(page);
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


        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var result = appService!.GetPaymentMethodById(id);
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
        [HttpPost("create")]
        public ActionResult Post([FromBody] PaymentMethod req)
        {
            var result = appService.CreateNew(req);
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
        public ActionResult Put([FromBody] PaymentMethod req)
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

