using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Api.ViewModels.Users
{
    public class ManageBalanceViewModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public decimal balanceToAdd { get; set; }
    }
}
