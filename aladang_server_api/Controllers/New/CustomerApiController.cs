using aladang_server_api.Configuration;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.Bo.Res;
using aladang_server_api.Services;
using ApiLotterySystem.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aladang_server_api.Models;

namespace aladang_server_api.Controllers.New;

[ApiController]
public class CustomerApiController : Controller
{
    private readonly ILogger<CustomerApiController> _logger;
    private ResponseMessage _responseMessage;
    private readonly AppDBContext _context;
    private readonly CustomerService _service;
    
    public CustomerApiController(ILogger<CustomerApiController> logger, AppDBContext context, CustomerService service)
    {
        _logger = logger;
        _service = service;
        _responseMessage = new ResponseMessage();
        _context = context;
    }
    
    [HttpPost("/api/v1/customer/create")]
    public ActionResult Create([FromBody] CustomerReq req)
    {
        _responseMessage = new ResponseMessage();
        _logger.LogInformation("Intercept create and update customer req : {}", req);
        try
        {
            var result = _service.CreateV(req);
            _responseMessage.SetCreateDataSuccess(result);
            return Ok(_responseMessage);
        }
        catch (AppException e)
        {
            _logger.LogError(e, "App Exception error create and update customer");
            return Ok(new ResponseMessage(e.Message, e.MessageKh, e.ErrorCode, null));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error general error create and update customer");
            _responseMessage.SetDataInternalServerError(null);
            return Ok(_responseMessage);
        }
    }
    
}