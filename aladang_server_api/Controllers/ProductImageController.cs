using System;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/productimage")]
    [ApiController, Authorize]
    public class ProductImageController : ControllerBase
    {
        private IProductImage appService;

        public ProductImageController(IProductImage appService)
        {
            this.appService = appService;
        }
        [HttpGet("all")]
        public IActionResult Get()
        {
            List<ProductImage> result = appService.GetAll();
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
            List<ProductImage> result = appService.GetProductImages(page);
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
            var result = appService!.GetProductImageById(id);
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
        public ActionResult Post([FromBody] ProductImage req)
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
        public IActionResult Put([FromBody] ProductImage req)
        {
            try
            {
                if (req == null || req.id == 0)
                {
                    return BadRequest(new
                    {
                        code = 400,
                        message = "Invalid request data"
                    });
                }

                var result = appService.Update(req);

                if (result != null)
                {
                    return Ok(new
                    {
                        code = 200,
                        message = "Data Updated Successfully",
                        count = 1,
                        countPage = 1,
                        currentPage = 1,
                        data = result
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        message = "ProductImage not found"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    code = 500,
                    message = "Error updating data: " + ex.Message
                });
            }
        }
    }

    /*  [HttpPut("update")]
      public ActionResult Put([FromBody] ProductImage req)
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
      }*/
}


