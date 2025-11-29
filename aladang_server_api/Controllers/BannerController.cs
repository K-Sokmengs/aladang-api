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


namespace aladang_server_api.Controllers
{
    [Route("/app/v1/banner")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private IBanner appService;

        public BannerController(IBanner appService)
        {
            this.appService = appService;
        }
        [HttpGet("all")]
        public IActionResult Get()
        {
            List<Banner> result = appService.GetAll(); 
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count= appService.Count(),
                countPage = appService.PageCount(),
                currentPage = 1,
                data = result

            });
        }

        [HttpGet("all/page")]
        public IActionResult Get(int page)
        {
            List<Banner> result = appService.GetBanners(page); 
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = 10,
                countPage = 1,
                currentPage = 1,
                data = result

            }) ;
        }


        [HttpGet("shopid/page")]
        public IActionResult GetByShopId(int shopid, int page)
        {
            List<Banner> result = appService!.GetByShopId(shopid, page);  
            if (result != null)
            {
                return Ok(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = appService.Count(shopid),
                    countPage = appService.PageCount(shopid),
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
        [HttpPost("create")]
        public ActionResult Post([FromBody] Banner req)
        {
            var result = appService.CreateNew(req);
            //var result = appService.GetById(req.id);
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
        public ActionResult Put([FromBody] Banner req)
        {
            var result = appService.Update(req);
            //var result = appService.GetById(req.id);
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

