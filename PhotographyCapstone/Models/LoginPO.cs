using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotographyCapstone.Models
{
    public class LoginPO
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}