using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HiGeekNews.UI.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "User name is wrong..!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is wrong..!")]
        public string Password { get; set; }
    }
}