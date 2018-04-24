using PhotographyCapstone.Mapping;
using PhotographyCapstone.Models;
using PhotographyCapstoneBLL;
using PhotographyCapstoneDAL;
using PhotographyCapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotographyCapstone.Custom
{
    public class CustomController : Controller
    {
        private readonly AlbumsDAO _dataAccess;
        private readonly UsersDAO _userDataAccess;
        private readonly PhotosDAO _photoDataAccess;

        public CustomController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new AlbumsDAO(connection);
            _userDataAccess = new UsersDAO(connection);
            _photoDataAccess = new PhotosDAO(connection);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SetBackgroundImageTempData();
        }


        public void SetBackgroundImageTempData()
        {
            int photoID;
            List<PhotoAlbumDO> junctionList = new List<PhotoAlbumDO>();
            try
            {
                junctionList = _dataAccess.ReadAllJunctions();
                PhotoAlbumBLO photoAlbumBLO = new PhotoAlbumBLO();
                photoID = photoAlbumBLO.PhotoInMostAlbums(PhotoAlbumMapper.ListPhotoAlbumDOtoPO(junctionList));
                PhotoPO mostCommonPhoto = PhotoMapper.PhotoDOtoPO(_photoDataAccess.ReadPhotoById(photoID));
                Session["BackgroundImageLocation"] = mostCommonPhoto.Photo;
            }
            catch (Exception ex)
            {
                LogFile.DataFile(ex: ex);
            }
            finally
            {

            }

        }
    }
}