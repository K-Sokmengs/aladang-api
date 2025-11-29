using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aladang_server_api.Services;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/exchange")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private IExchange appService;

        public ExchangeController(IExchange exchangeService)
        {
            this.appService = exchangeService;
        }
        [HttpGet("all")]
        public IActionResult Get()
        {

            List<Exchange> result = appService.GetAll(); 
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
            List<Exchange> result = appService.GetExchange(page);
            double pagecount = appService.PageCount(); 
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

        [HttpGet, Route("shopid")]
        public IActionResult GetExchangeRateByShop(int shopid)
        {
            Exchange result = appService.GetExchangeRateByShop(shopid);  
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
        [HttpGet, Route("shopid/all")]
        public IActionResult GetByShopId(int shopid)
        {
            List<Exchange> result = appService.GetAllByShopId(shopid);
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

        [HttpGet("max-exchange")]
        public IActionResult GetMaxExchange(int shopid)
        {
            var result = appService!.GetMax(shopid);
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
        public ActionResult Post([FromBody] Exchange req)
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
        public ActionResult Put([FromBody] Exchange req)
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

