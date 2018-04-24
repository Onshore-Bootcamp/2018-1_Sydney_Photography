using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyCapstone.Models
{
    public class UserAlbumPO
    {
        public AlbumPO AlbumPO { get; set; }

        public UsersPO UsersPO { get; set; }

        public UserAlbumPO()
        {
            UsersPO = new UsersPO();
        }
    }
}