using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aladang_server_api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private ICurrency appService;

        public CurrencyController(ICurrency currencyService)
        {
            this.appService = currencyService;
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            List<Currency> result = appService.GetAll(); 
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
            List<Currency> result = appService.GetCurrency(page); 
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

        [HttpGet("status/page")]
        public IActionResult GetByStatus(string status, int page)
        {
            List<Currency> result = appService.GetByStatus(status, page);
            double pagecount = appService.PageCount(status);
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
        public ActionResult Post([FromBody] Currency req)
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
        public ActionResult Put([FromBody] Currency req)
        {
            var result =appService.Update(req);
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

