using PhotographyCapstone.Models;
using PhotographyCapstoneBLL.Models;
using PhotographyCapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotographyCapstone.Mapping
{
    public class PhotoMapper
    {
        public static  PhotoPO PhotoDOtoPO(PhotoDO from)
        {
            PhotoPO to = new PhotoPO();
            to.PhotoID = from.PhotoID;
            to.PhotoName = from.PhotoName;
            to.Height = from.Height;
            to.Width = from.Width;
            to.ExtensionType = from.ExtensionType;
            to.Size = from.Size;
            to.DateCreated = from.DateCreated;
            to.Photo = from.Photo;
            to.Byte = from.Byte;
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;
            return to;
        }

        public static PhotoDO PhotoPOtoDO(PhotoPO from)
        {
            PhotoDO to = new PhotoDO();
            to.PhotoID = from.PhotoID;
            to.PhotoName = from.PhotoName;
            to.Height = from.Height;
            to.Width = from.Width;
            to.ExtensionType = from.ExtensionType;
            to.Size = from.Size;
            to.DateCreated = from.DateCreated;
            to.Photo = from.Photo;
            to.Byte = from.Byte;
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;

            return to;
        }

        public static  PhotoBO PhotoPOtoBO (PhotoPO from)
        {
            PhotoBO to = new PhotoBO();
            to.PhotoID = from.PhotoID;
            to.PhotoName = from.PhotoName;
            to.Height = from.Height;
            to.Width = from.Width;
            to.ExtensionType = from.ExtensionType;
            to.Size = from.Size;
            to.DateCreated = from.DateCreated;
            to.Photo = from.Photo;
            to.Byte = from.Byte;
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;

            return to;
        }

        public static  PhotoBO PhotoDOtoBO(PhotoDO from)
        {
            PhotoBO to = new PhotoBO();
            to.PhotoID = from.PhotoID;
            to.PhotoName = from.PhotoName;
            to.Height = from.Height;
            to.Width = from.Width;
            to.ExtensionType = from.ExtensionType;
            to.Size = from.Size;
            to.DateCreated = from.DateCreated;
            to.Photo = from.Photo;
            to.Byte = from.Byte;
            to.AlbumID = from.AlbumID;
            to.AlbumName = from.AlbumName;

            return to;

        }
        public static List<PhotoPO> ListPhotoDOtoPO(List<PhotoDO> from)
        {
            List<PhotoPO> to = new List<PhotoPO>();
            from.ForEach((photoFrom) => to.Add(PhotoDOtoPO(photoFrom)));
            return to;
        }
    }
}