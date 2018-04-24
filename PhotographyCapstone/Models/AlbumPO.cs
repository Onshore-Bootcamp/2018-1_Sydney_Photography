using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyCapstone.Models
{
    public class AlbumPO
    {
        public int AlbumID { get; set; }

        [Required]
        [Display(Name ="Album Name")]
        public string AlbumName { get; set; }
               
        [Required]
        [Display(Name ="Album Type")]
        public string AlbumType { get; set; }

        public long? UserID { get; set; }

        public int PhotoID { get; set; }

        public string Photo { get; set; }

        [Display(Name ="Photo Name")]
        public string PhotoName { get; set; }

        public List<SelectListItem> UserSelectList { get; set; }

        

       

    }
}