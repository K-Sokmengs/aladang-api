using System;
namespace aladang_server_api.Models.BO.Req
{
	public class ResetCustomerPassword
	{
        public string? phone { get; set; }
        public string? customerName { get; set; }
        public string? newPassword { get; set; }
    }
}

