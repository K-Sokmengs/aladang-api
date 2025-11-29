using aladang_server_api.Configuration;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.Bo.Res;
using aladang_server_api.Services;
using aladang_server_api.Utils;
using ApiLotterySystem.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aladang_server_api.Controllers.New;
[ApiController]
[Authorize]
public class ExchangeRateApiController : Controller
{
    private readonly ILogger<ExchangeRateApiController> _logger;
    private ResponseMessage _responseMessage;
    private readonly AppDBContext _context;
    private readonly CustomerService _service;
    
    public ExchangeRateApiController(ILogger<ExchangeRateApiController> logger, AppDBContext context, CustomerService service)
    {
        _logger = logger;
        _service = service;
        _responseMessage = new ResponseMessage();
        _context = context;
    }
    
    [HttpPost("/api/v1/exchangeRate/generateTotalAmount")]
    public ActionResult GenerateTotalAmount([FromBody] GenerateTotalAmountRequest req)
    {
        _responseMessage = new ResponseMessage();
        _logger.LogInformation("Intercept generate total Amount req : {}", req);
        try
        {
            var generateTotalAmountResponse = new GenerateTotalAmountResponse();
           
            var exchangeResult = _context.exchanges?.Where(c => c.shopid == req.ShopId && c.currencyid==req.CurrencyId).OrderByDescending(c => c.date)
                .FirstOrDefault();
            if (exchangeResult == null)
            {
                throw new AppException
                {
                    ErrorCode = "400",
                    Message = "Exchange rate not found",
                    MessageKh = "Exchange rate not found",
                    HttpStatus = 200
                };
            }
            var currency = _context.currencies?.SingleOrDefault(c => c.id == exchangeResult.currencyid);
            if (currency == null)
            {
                throw new AppException
                {
                    ErrorCode = "400",
                    Message = "Shop don't have currency",
                    MessageKh = "Shop don't have currency",
                    HttpStatus = 200
                };
            }
            generateTotalAmountResponse.TotalAmountFormat = currency.sign+" "+ AmountFormatCurrencyUtil.GetAmountFormatCurrency(exchangeResult.rate * req.TotalAmount??0,currency.currencyname ?? "USD");
            generateTotalAmountResponse.TotalAmount = exchangeResult.rate * req.TotalAmount;
            generateTotalAmountResponse.ExchangeRateAmount = "$1=" +currency.sign + exchangeResult.rate;
            generateTotalAmountResponse.Currency = currency.sign;
            generateTotalAmountResponse.ExchangeRate = exchangeResult.rate;
            _responseMessage.SetCreateDataSuccess(generateTotalAmountResponse);
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