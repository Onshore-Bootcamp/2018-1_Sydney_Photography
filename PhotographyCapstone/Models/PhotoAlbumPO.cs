using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyCapstone.Models
{
    public class PhotoAlbumPO
    {
        public List<SelectListItem> DropDown { get; set; }

        public int PhotoID { get; set; }

        public int AlbumID { get; set; }

        public long PhotoAlbumID { get; set; }
        
    }
}