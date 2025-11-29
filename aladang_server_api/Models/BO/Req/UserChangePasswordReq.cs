using System;
using System.ComponentModel.DataAnnotations;

namespace aladang_server_api.Models.BO.Req
{
	public class UserChangePasswordReq
	{
        public int userId { get; set; }
        [Required(ErrorMessage = "Current password is required")]
        public string currentPassword { get; set; }
        [Required(ErrorMessage = "New password is required")]
        public string newPassword { get; set; } 
        [Required(ErrorMessage = "Confirm password is required!")]
        [Compare("newPassword", ErrorMessage = "Confirm new password does not match!")]
        public string confirmPassword { get; set; }
    }
}

