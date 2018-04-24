using PhotographyCapstone.Models;
using PhotographyCapstoneBLL.Models;
using PhotographyCapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotographyCapstone.Mapping
{
    public class AlbumMapper
    {
        public static AlbumPO AlbumDOtoPO(AlbumDO from)
        {
            AlbumPO to = new AlbumPO();
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;
            to.AlbumType = from.AlbumType;
            to.UserID = from.UserID;
            to.PhotoID = from.PhotoID;
            to.Photo = from.Photo;
            to.PhotoName = from.PhotoName;

            return to;
        }

        public static AlbumDO AlbumPOtoDO(AlbumPO from)
        {
            AlbumDO to = new AlbumDO();
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;
            to.AlbumType = from.AlbumType;
            to.UserID = from.UserID;
            to.PhotoID = from.PhotoID;
            to.Photo = from.Photo;
            to.PhotoName = from.PhotoName;
            return to;

        }

        public static AlbumBO AlbumPOtoBO(AlbumPO from)
        {
            AlbumBO to = new AlbumBO();
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;
            to.AlbumType = from.AlbumType;
            to.UserID = from.UserID;
            to.PhotoID = from.PhotoID;
            to.Photo = from.Photo;
            to.PhotoName = from.PhotoName;
            return to;
        }

        public static AlbumBO AlbumDOtoBO(AlbumDO from)
        {
            AlbumBO to = new AlbumBO();
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;
            to.AlbumType = from.AlbumType;
            to.UserID = from.UserID;
            to.PhotoID = from.PhotoID;
            to.Photo = from.Photo;
            to.PhotoName = from.PhotoName;
            return to;
        }
        public static List<AlbumPO> ListAlbumDOtoPO(List<AlbumDO> from)
        {
            List<AlbumPO> to = new List<AlbumPO>();
            from.ForEach((albumFrom) => to.Add(AlbumDOtoPO(albumFrom)));
            return to;
        }
    }
}