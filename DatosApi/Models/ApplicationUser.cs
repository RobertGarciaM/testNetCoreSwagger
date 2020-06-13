using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Api.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Nationality { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string CompletName { get; set; }
        [Column(TypeName = "Money")]
        public decimal Balance { get; set; }
    }
}
