using PhotographyCapstone.Models;
using PhotographyCapstoneBLL.Models;
using PhotographyCapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotographyCapstone.Mapping
{
    public class PhotoAlbumMapper
    {
        public static PhotoAlbumPO PhotoAlbumDOtoPO(PhotoAlbumDO from)
        {
            PhotoAlbumPO to = new PhotoAlbumPO();
            to.PhotoAlbumID = from.PhotoAlbumID;
            to.PhotoID = from.PhotoID;
            to.AlbumID = from.AlbumID;

            return to;
        }

        public static PhotoAlbumDO PhotoAlbumPOtoDO(PhotoAlbumPO from)
        {
            PhotoAlbumDO to = new PhotoAlbumDO();
            to.PhotoAlbumID = from.PhotoAlbumID;
            to.PhotoID = from.PhotoID;
            to.AlbumID = from.AlbumID;

            return to;
        }

        public static PhotoAlbumBO PhotoAlbumDOtoBO(PhotoAlbumDO from)
        {
            PhotoAlbumBO to = new PhotoAlbumBO();
            to.PhotoAlbumID = from.PhotoAlbumID;
            to.PhotoID = from.PhotoID;
            to.AlbumID = from.AlbumID;

            return to;
        }
        public static PhotoAlbumPO PhotoAlbumBOtoPO(PhotoAlbumBO from)
        {
            PhotoAlbumPO to = new PhotoAlbumPO();
            to.PhotoAlbumID = from.PhotoAlbumID;
            to.PhotoID = from.PhotoID;
            to.AlbumID = from.AlbumID;

            return to;
        }

        public static List<PhotoAlbumBO> ListPhotoAlbumDOtoPO(List<PhotoAlbumDO> from)
        {
            List<PhotoAlbumBO> to = new List<PhotoAlbumBO>();
            from.ForEach((photoAlbumFrom) => to.Add(PhotoAlbumDOtoBO(photoAlbumFrom)));
            return to;
        }
    }
}

