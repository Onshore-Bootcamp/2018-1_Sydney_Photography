using PhotographyCapstone.Custom;
using PhotographyCapstone.Mapping;
using PhotographyCapstone.Models;
using PhotographyCapstoneDAL;
using PhotographyCapstoneDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographyCapstone.Controllers
{
    public class PhotoController : CustomController
    {
        public PhotoController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new PhotosDAO(connection);
            _albumsDataAccess = new AlbumsDAO(connection);
            SetBackgroundImageTempData();
        }

        private readonly PhotosDAO _dataAccess;
        private readonly AlbumsDAO _albumsDataAccess;


        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, PhotoPO form)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        if (file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string path = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                            string relativePath = "/" + path.Replace(Server.MapPath("/"), "").Replace("\\", "/");
                            file.SaveAs(path);
                            form.Photo = relativePath;
                            form.DateCreated = DateTime.Now;
                            _dataAccess.AddPhoto(PhotoMapper.PhotoPOtoDO(form));
                            response = RedirectToAction("ViewAllPhotos", "Photo");
                        }

                        else
                        {
                            response = View(form);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogFile.DataFile(ex: ex);
                        throw ex;
                    }
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
        public ActionResult ViewAllPhotos()
        {
            ActionResult response = null;
            List<PhotoDO> photoList = new List<PhotoDO>();
            List<List<PhotoPO>> listPhotos = new List<List<PhotoPO>>();
            if (Session["RoleID"] != null && (byte)Session["RoleID"] >= 1)
            {
                try
                {
                    photoList = _dataAccess.ViewAllPhotos();
                    for (int i = 0; i < (float)photoList.Count / 4; i++)
                    {
                        listPhotos.Add(PhotoMapper.ListPhotoDOtoPO(photoList.Skip(i * 4).Take(4).ToList()));
                    }
                }
                catch (Exception ex)
                {
                    LogFile.DataFile(ex: ex);
                }
                finally
                {

                }
                response = View(listPhotos);
            }
            else
            {
                TempData["Statement"] = "Please register to view page you are requesting.";
                response = RedirectToAction("Index", "Home");

            }
            return response;
        }

        [HttpGet]
        public ActionResult UpdatePhoto(int photoID)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                try
                {
                    response = View(PhotoMapper.PhotoDOtoPO(_dataAccess.ReadPhotoById(photoID)));
                }
                catch (Exception ex)
                {
                    LogFile.DataFile(ex: ex);
                    throw ex;
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
        public ActionResult UpdatePhoto(PhotoPO form)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleId"] == 3)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _dataAccess.UpdatePhoto(PhotoMapper.PhotoPOtoDO(form));
                        response = RedirectToAction("ViewAllPhotos", "Photo");

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

        [HttpGet]
        public ActionResult RemovePhoto(int photoID)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] >= 2)
            {
                try
                {
                    _dataAccess.DeletePhoto(photoID);
                    response = RedirectToAction("ViewAllPhotos", "Photo");
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

        public ActionResult ViewPhotoDetails(int photoID)
        {
            ActionResult response= null;
            PhotoDO photo = new PhotoDO();
            if (Session["RoleID"] != null)
            {
                try
                {
                    photo = _dataAccess.ReadPhotoById(photoID);
                    response = View(PhotoMapper.PhotoDOtoPO(photo));
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
                TempData["Statement"] = "Please register to view this page.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        

    }
}