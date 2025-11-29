using System;
using System.ComponentModel.DataAnnotations;

namespace aladang_server_api.Models.BO.Req
{
	public class ShopReq
	{
        public int id { get; set; }
        public string userType { get; set; } = "shop";
        public string? shopid { get; set; }
        public string? shopName { get; set; }
        public string? gender { get; set; }
        public DateTime dob { get; set; }
        public string? nationality { get; set; }
        public string? ownerName { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? tokenid { get; set; }
        public string? facebookPage { get; set; }
        public string? location { get; set; }
        public string? logoShop { get; set; }
        public string? paymentType { get; set; }
        public string? qrCodeImage { get; set; }
        public int? bankNameid { get; set; }
        public string? accountNumber { get; set; }
        public string? accountName { get; set; }
        public string? feetype { get; set; }
        public decimal? feecharge { get; set; }
        public DateTime? shophistorydate { get; set; }
        public string? note { get; set; }
        public string? status { get; set; }
        public string? idcard { get; set; }
        public DateTime expiredate { get; set; }

    }

    public class ChangePasswordShop
    {
        public int? shopid { get; set; } 
        public string? phone { get; set; }
        [Required(ErrorMessage = "Current password is required!")]
        public string? currentPassword { get; set; }
        [Required(ErrorMessage = "New password is required!")]
        public string? newPassword { get; set; }
        [Required(ErrorMessage = "Confirm new password is required!")]
        [Compare("newPassword", ErrorMessage = "Confirm new password does not match!")]
        public string? confirmNewPassword { get; set; }
    }


    public class ResetPasswordShop
    {
        public string? shopName { get; set; }
        public string? phone { get; set; }
        public string? idcard { get; set; }
        public DateTime expiredate { get; set; }
        public string? newPassword { get; set; }
        public string? confirmNewPassword { get; set; }
    }

    public class ChangeLogoShop
    {
        public int? shopid { get; set; }
        public string? newlogo { get; set; } 
    }


    public class ChangeLogoQRCode
    {
        public int? shopid { get; set; }
        public string? newqr { get; set; }
    }
}

