using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aladang_server_api.Models.AppAuthorize
{
    public class AuthenticateRequest
    {
        [Required]
        [DefaultValue("adminApi2023")]
        public string? Username { get; set; } = "adminApi2023";

        [Required]
        [DefaultValue("Pass@777s;ldjfdjwyew(76676655")]
        public string? Password { get; set; }
    }
}

