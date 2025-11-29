using aladang_server_api.Configuration;
using aladang_server_api.Models;
using aladang_server_api.Models.Bo.Res;
using aladang_server_api.Services;
using ApiLotterySystem.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aladang_server_api.Controllers.New;

[ApiController]
public class ShopApiController : Controller
{
    private readonly ILogger<ShopApiController> _logger;
    private ResponseMessage _responseMessage;
    private readonly AppDBContext _context;
    private readonly ShopService _shopService;
    
    public ShopApiController(ILogger<ShopApiController> logger, AppDBContext context, ShopService shopService)
    {
        _logger = logger;
        _shopService = shopService;
        _responseMessage = new ResponseMessage();
        _context = context;
    }
    
    [HttpPost("/api/v1/shop/create")]
    public ActionResult Create([FromBody] Shop req)
    {
        _responseMessage = new ResponseMessage();
        _logger.LogInformation("Intercept create and update shop req : {}", req);
        try
        {
            var result = _shopService.CreateNewV1(req);
            _responseMessage.SetCreateDataSuccess(result);
            return Ok(_responseMessage);
        }
        catch (AppException e)
        {
            _logger.LogError(e, "App Exception error create and update shop");
            return Ok(new ResponseMessage(e.Message, e.MessageKh, e.ErrorCode, null));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error general error create and update shop");
            _responseMessage.SetDataInternalServerError(null);
            return Ok(_responseMessage);
        }
    }
}