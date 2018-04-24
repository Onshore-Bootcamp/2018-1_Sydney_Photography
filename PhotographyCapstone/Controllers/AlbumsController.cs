using PhotographyCapstone.Custom;
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

namespace PhotographyCapstone.Controllers
{
    public class AlbumsController : CustomController
    {
        private readonly AlbumsDAO _dataAccess;
        private readonly UsersDAO _userDataAccess;
        private readonly PhotosDAO _photoDataAccess;


        public AlbumsController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new AlbumsDAO(connection);
            _userDataAccess = new UsersDAO(connection);
            _photoDataAccess = new PhotosDAO(connection);
            SetBackgroundImageTempData();
        }


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddAlbum()
        {
            ActionResult response;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                response = View();
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        [HttpPost]
        public ActionResult AddAlbum(AlbumPO form)
        {

            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _dataAccess.AddAlbum(AlbumMapper.AlbumPOtoDO(form));
                        response = RedirectToAction("ViewAlbums", "Albums");

                    }
                    catch (Exception ex)
                    {
                        LogFile.DataFile(ex: ex);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    response = View(form);
                }
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        public List<SelectListItem> FillUsersListDropDown()
        {
            List<SelectListItem> UserSelectList = new List<SelectListItem>();
            List<UsersPO> users = UserMapper.ListUsersDOtoPO(_userDataAccess.ReadALLUsers());

            foreach (UsersPO user in users)
            {
                UserSelectList.Add(new SelectListItem { Text = user.UserName, Value = user.UserID.ToString() });
            }

            return UserSelectList;
        }
        [HttpGet]
        public ActionResult UpdateAlbum(int albumID)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                try
                {
                    AlbumPO useralbums = new AlbumPO();
                    useralbums = AlbumMapper.AlbumDOtoPO(_dataAccess.ReadAlbumByID(albumID));
                    useralbums.UserSelectList = FillUsersListDropDown();

                    response = View(useralbums);
                }
                catch (Exception ex)
                {
                    LogFile.DataFile(ex: ex);
                }
                finally
                {

                }
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;

        }

        [HttpPost]
        public ActionResult UpdateAlbum(AlbumPO form)
        {
            AlbumPO album = new AlbumPO();
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        _dataAccess.UpdateAlbum(AlbumMapper.AlbumPOtoDO(form));
                        response = RedirectToAction("ViewAlbums", "Albums");
                    }
                    catch (Exception ex)
                    {
                        LogFile.DataFile(ex: ex);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    FillUsersListDropDown();
                    response = View(form);
                }
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        [HttpGet]
        public ActionResult ViewAlbums()
        {
            ActionResult response = null;
            List<AlbumDO> albumList = new List<AlbumDO>();
            List<List<AlbumPO>> listalbums = new List<List<AlbumPO>>();
            if (Session["RoleID"] != null && (byte)Session["RoleID"] >= 1)
            {
                try
                {
                    albumList = _dataAccess.ReadAllAlbums();
                   
                    for(int i = 0; i<(float)albumList.Count/3; i++)
                    {
                        listalbums.Add(AlbumMapper.ListAlbumDOtoPO(albumList.Skip(i * 3).Take(3).ToList()));
                    }
                }
                catch (Exception ex)
                {
                    LogFile.DataFile(ex: ex);
                }
                finally
                {

                }
                response = View(listalbums);
            }
            else
            {
                TempData["Statement"] = "Please register to view this page.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        [HttpGet]
        public ActionResult ViewPhotosInAlbum(int albumID)
        {
            ActionResult response = null;
            List<AlbumDO> photoAlbumList = new List<AlbumDO>();
            if (Session["RoleId"] != null && (byte)Session["RoleID"] >= 1)
            {
                try
                {
                    photoAlbumList = _dataAccess.ViewPhotosInAlbum(albumID);
                }
                catch (Exception ex)
                {
                    LogFile.DataFile(ex: ex);
                }
                finally
                {

                }
                response = View(AlbumMapper.ListAlbumDOtoPO(photoAlbumList));

            }
            else
            {
                TempData["Statement"] = "Please register to view this page.";
                response = RedirectToAction("Index", "Home");
            }

            return response;
        }

        [HttpGet]
        public ActionResult RemoveAlbum(int albumID)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {

                try
                {
                    _dataAccess.DeleteAlbum(albumID);
                    response = RedirectToAction("ViewAlbums", "Albums");
                }
                catch (Exception ex)
                {
                    LogFile.DataFile(ex: ex);
                }
                finally
                {

                }
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        public List<SelectListItem> FillAlbumsListDropDown()
        {
            List<AlbumPO> albums = AlbumMapper.ListAlbumDOtoPO(_dataAccess.ReadAllAlbums());
            List<SelectListItem> AlbumSelectList = new List<SelectListItem>();
            foreach (AlbumPO album in albums)
            {
                AlbumSelectList.Add(new SelectListItem { Text = album.AlbumName, Value = album.AlbumID.ToString() });
            }

            return AlbumSelectList;
        }

        [HttpGet]
        public ActionResult AddPhotoToAlbum(int photoID)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                PhotoAlbumPO vm = new PhotoAlbumPO();
                vm.DropDown = FillAlbumsListDropDown();

                response = View(vm);
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }
        [HttpPost]
        public ActionResult AddPhotoToAlbum(PhotoAlbumPO form)
        {
            ActionResult response = null;
            if (Session["RoleId"] != null && (byte)Session["RoleID"] == 3)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _dataAccess.AddPhotoToAlbum(form.PhotoID, form.AlbumID);

                        response = RedirectToAction("ViewAlbums", "Albums");
                    }
                    catch (Exception ex)
                    {
                        LogFile.DataFile(ex: ex);
                    }
                    finally
                    {

                    }
                }
                else
                {
                    PhotoAlbumPO vm = new PhotoAlbumPO();
                    vm.DropDown = FillAlbumsListDropDown();
                    response = View(vm);
                }
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }





    }
}