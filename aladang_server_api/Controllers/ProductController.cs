using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using aladang_server_api.Interface;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aladang_server_api.Models;
using aladang_server_api.Services;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProduct appService;

        public ProductController(IProduct productService)
        {
            this.appService = productService;
        }
        [HttpGet("all")]
        public IActionResult Get()
        {
            List<ProductRes> result = appService.GetAll();
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
            List<ProductRes> result = appService.GetProduct(page);
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

        [HttpGet("shopid")]
        public IActionResult GetByShopId(int shopid)
        {
            List<ProductRes> result = appService.GetByShopId(shopid);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.Count(shopid),
                countPage = 1,
                currentPage = 1,
                data = result

            });
        }


        [HttpGet("shopid/page/status")]
        public IActionResult GetByShopId(int shopid,int page, string status)
        {
            List<ProductRes> result = appService.GetByShopId(shopid,page, status); 
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = appService.Count(shopid, status),
                countPage = appService.PageCount(shopid, status),
                currentPage = page,
                data = result

            });
        }



        [HttpGet("status/page")]
        public IActionResult GetByStatus(string status, int page)
        {
            List<ProductRes> result = appService!.GetByStatus(status, page);
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

            });
        }

        

        // POST api/values
        [HttpPost("create")]
        [Authorize]
        public ActionResult Post([FromBody] ProductReq req)
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

        [HttpPost("update")]
        public ActionResult Put([FromBody] ProductReq req)
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

