using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using aladang_server_api.Configuration;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aladang_server_api.Controllers
{
    [Route("/app/v1/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocation appService;
        public LocationController(ILocation locationService)
        {
            this.appService = locationService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            List<Locations> result = appService.GetAll();
            double count = appService.Count();
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
        public IActionResult GetLocation(int page)
        {
            List<Locations> result = appService.GetLocations(page);  
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
        public IActionResult GetStatusPage(string status, int page)
        {
            List<Locations> result = appService.GetByStatus(status,page); 
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
        public IActionResult Post([FromBody] Locations req)
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
        public ActionResult Put([FromBody] Locations req)
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

