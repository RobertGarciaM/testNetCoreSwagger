using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Api.ViewModels.Users
{
    public class ApplicationUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string CompletName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Rol { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}
