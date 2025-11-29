using System;

namespace ApiLotterySystem.Exceptions;

public class AppException : Exception
{
    public AppException(string message, string messageKh, string errorCode, int httpStatus)
    {
        HttpStatus = httpStatus;
        ErrorCode = errorCode;
        Message = message;
        MessageKh = messageKh;
    }

    public AppException()
    {
    }
    public AppException(string message, string messageKh)
    {
        HttpStatus = 200;
        ErrorCode = "400";
        Message = message;
        MessageKh = messageKh;
    }
    

    public int HttpStatus { get; set; }
    public string Message { get; set; }
    public string MessageKh { get; set; }
    public string ErrorCode { get; set; }
}