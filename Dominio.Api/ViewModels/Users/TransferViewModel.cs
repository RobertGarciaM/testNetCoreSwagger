using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Api.ViewModels.Users
{
    public class TransferViewModel
    {
        [Required]
        public string userNameToTransfer { get; set; }
        [Required]
        public decimal balance { get; set; }
    }
}
