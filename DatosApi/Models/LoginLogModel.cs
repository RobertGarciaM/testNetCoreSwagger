using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Api.Models
{
    [Table("LogLogin", Schema = "dbo")]
    public class LoginLogModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string userName { get; set; }
        [Required]
        public DateTime dateLogin { get; set; }
    }
}
