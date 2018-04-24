using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyCapstone.Models
{
    public class UsersPO
    {
        [Display(Name = "User ID")]
        public long UserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
                
        public string Email { get; set; }

        public string City { get; set; }

        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name ="Role ID")]
        public byte RoleID { get; set; }

        public string RoleName { get; set; }

        public List<SelectListItem> RoleIDList { get; set; }

        public List<SelectListItem> UserSelectList { get; set; }


        public UsersPO()
        {
            UserSelectList = new List<SelectListItem>();
        }
    }
}