using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Api.ViewModels.Role
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
