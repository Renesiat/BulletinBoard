﻿using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Web.Models
{
    public class LoginViewModel
    {
            [Required]
            [Display(Name = "Username")]
            public  string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public  string Password { get; set; }
    }
}
