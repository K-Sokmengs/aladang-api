namespace aladang_server_api.Models.Bo.Res;

public class ResponseMessage
{
    public ResponseMessage()
    {
    }

    public ResponseMessage(string message, string messageKh, string errorCode, object data)
    {
        Message = message;
        MessageKh = messageKh;
        ErrorCode = errorCode;
        Data = data;
    }

    public string Message { get; set; }
    public string MessageKh { get; set; }
    public string ErrorCode { get; set; }
    public object Data { get; set; }
    public int TotalPage { get; set; }

    public ResponseMessage GetDataSuccess(object data)
    {
        var response = new ResponseMessage
        {
            Message = "Get Data Successful!",
            MessageKh = "ទទួលបានទិន្នន័យជោគជ័យ!",
            ErrorCode = "200",
            Data = data
        };
        return response;
    }

    public void SetDataSuccess(object data, int? totalPage)
    {
        Message = "Get Data Successful!";
        MessageKh = "ទទួលបានទិន្នន័យជោគជ័យ!";
        ErrorCode = "200";
        Data = data;
        TotalPage = totalPage ?? 0;
    }

    public void SetDataInternalServerError(object data)
    {
        Message = "Internal Server Error!";
        MessageKh = "Internal Server Error!";
        ErrorCode = "500";
        Data = data;
    }


    public ResponseMessage SetCreateSuccess(object data)
    {
        var response = new ResponseMessage
        {
            Message = "Create Successful!",
            MessageKh = "បង្កើតជោគជ័យ!",
            ErrorCode = "200",
            Data = data
        };
        return response;
    }

    public ResponseMessage SetUpdateSuccess(object data)
    {
        var response = new ResponseMessage
        {
            Message = "Update Successful!",
            MessageKh = "អាប់ដេតបានជោគជ័យ!",
            ErrorCode = "200",
            Data = data
        };
        return response;
    }

    public ResponseMessage SetDeleteSuccess(object data)
    {
        var response = new ResponseMessage
        {
            Message = "Create Successful!",
            MessageKh = "បង្កើតជោគជ័យ!",
            ErrorCode = "200",
            Data = data
        };
        return response;
    }

    public ResponseMessage SetDataNotFound(object data)
    {
        var response = new ResponseMessage
        {
            Message = "Data Not found!",
            MessageKh = "រកមិនឃើញទិន្នន័យ!",
            ErrorCode = "400",
            Data = data
        };
        return response;
    }


    public ResponseMessage InternalServer(object data)
    {
        var response = new ResponseMessage
        {
            Message = "Internal Server Error",
            MessageKh = "កំហុសម៉ាស៊ីនមេខាងក្នុង",
            ErrorCode = "500",
            Data = data
        };
        return response;
    }

    public ResponseMessage AlreadyCreate(object data)
    {
        var response = new ResponseMessage
        {
            Message = "Create already exit",
            MessageKh = "បង្កើតរួចហើយ",
            ErrorCode = "400",
            Data = data
        };
        return response;
    }

    public ResponseMessage CustomMessage(int statusCode, object obj, string message)
    {
        var responseMessage = "Data Get Successful!";
        var response = new ResponseMessage();

        if (message != "") responseMessage = message;

        switch (statusCode)
        {
            case 200:
                response.Message = "Data Get Successful!";
                response.ErrorCode = "ERROR_000";
                response.Data = obj;
                return response;
            case 404:
                response.Message = "Data Get Not Found!";
                response.ErrorCode = "ERROR_404";
                response.Data = null;
                return response;
            case 500:
                response.Message = "Internal Server Error!";
                response.ErrorCode = "ERROR_404";
                response.Data = null;
                return response;
            case 403:
                response.Message = "User is block!";
                response.ErrorCode = "ERROR_403";
                response.Data = null;
                return response;
            default:
                return null;
        }
    }

    public void SetUpdateDataSuccess(object data)
    {
        Message = "Update Successful!";
        MessageKh = "អាប់ដេតបានជោគជ័យ!";
        ErrorCode = "200";
        Data = data;
    }

    public void SetUpdateDataUnSuccess(object data)
    {
        Message = "Update Un Successful!";
        MessageKh = "អាប់ដេតបានជោគជ័យ!";
        ErrorCode = "200";
        Data = data;
    }

    public void SetCreateDataSuccess(object data)
    {
        Message = "Create Successful!";
        MessageKh = "បង្កើតជោគជ័យ!";
        ErrorCode = "200";
        Data = data;
    }
    
    public void SetRequestDeleteSuccess(object data)
    {
        Message = "Request Delete Data Successful!";
        MessageKh = "សំណើលុបទិន្នន័យជោគជ័យ!";
        ErrorCode = "200";
        Data = data;
    }
    
    public void CanNoPlay(object data)
    {
        Message = "Can not play this data time";
        MessageKh = "មិនអាចលេងពេលវេលាទិន្នន័យនេះបានទេ។";
        ErrorCode = "400";
        Data = data;
    }
    
    public void CanNotDeletePlay(object data)
    {
        Message = "Can not delete this data time";
        MessageKh = "មិនអាចលុបពេលវេលាទិន្នន័យនេះបានទេ។";
        ErrorCode = "400";
        Data = data;
    }
    
}