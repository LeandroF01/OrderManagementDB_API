﻿using System.ComponentModel.DataAnnotations;

namespace OrderManagementDB_API.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
