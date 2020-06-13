using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Api.Models
{
    public class ApplicationUserDeleteViewModel
    {
        [Required]
        public string userName { get; set; }
    }
}
