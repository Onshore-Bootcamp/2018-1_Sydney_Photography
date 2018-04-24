using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyCapstone.Models
{
    public class PhotoPO
    {
        public Int32 PhotoID { get; set; }

        [Required]
        [Display(Name = "Photo Name")]
        public string PhotoName { get; set; }

        [Required]
        public Int32 Height { get; set; }

        [Required]
        public Int32 Width { get; set; }

        [Required]
        [Display(Name = "Extension Type")]
        public string ExtensionType { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        [Display(Name = "Date Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated { get; set; }

        public string Photo { get; set; }

        public Byte?[] Byte { get; set; }

        public Int32 AlbumID { get; set; }

        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }

       
    }
}