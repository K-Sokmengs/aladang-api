using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using aladang_server_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerService appService;
        public CustomerController(CustomerService _appService)
        {
            this.appService = _appService;
        }
        [HttpGet, Route("all")]
        public IActionResult Get()
        { 
            List<Customer> result = appService.GetAll(); 
            if (result != null)
            {
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
            return NotFound(new
            {
                code = 404,
                message = "Data Not Found!",
                data = result
            });
        }

        [HttpGet, Route("all/page")]
        public IActionResult Get(int page)
        {
            List<Customer> result = appService.GetCustomers(page); 
            if (result != null)
            {
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
            return NotFound(new
            {
                code = 404,
                message = "Data Not Found!",
                data = result 
            }); 
        }


        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            CustomerRes result = appService.GetById(id);
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
        public ActionResult Post([FromBody] CustomerReq req)
        {
            var result=appService.Create(req); 
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
        public ActionResult Put([FromBody] Customer req)
        {
            var result=appService.Update(req);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                data = result 
            });
        }


        [HttpPut("changepassword")]
        public IActionResult changepassword([FromBody] UserChangePasswordReq req)
        {
            var result = appService.UpdateCustomerChangePwd(req);

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


        [HttpPut("resetpassword")]
        public IActionResult resetpassword([FromBody] ResetCustomerPassword req)
        {
            var result = appService.ResetCustomerPassword(req);
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
            return NotFound();
        }


        [HttpPut("updateImageProfile")]
        public IActionResult ChangeUpdatePhoto([FromForm] UpdatePhotoUser req)
        {
            var resutl = appService.ChangeImageProfile(req);
            return Ok(new
            {
                code = 200,
                message = "Data Successfully",
                count = 1,
                countPage = 1,
                currentPage = 1,
                data = resutl 
            }); 
        } 
    }
}
