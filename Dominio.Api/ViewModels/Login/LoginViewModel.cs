using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Api.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
