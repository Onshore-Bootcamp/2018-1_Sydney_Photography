using PhotographyCapstone.Custom;
using PhotographyCapstone.Mapping;
using PhotographyCapstone.Models;
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
    public class HomeController : CustomController
    {

        public HomeController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new UsersDAO(connection);
           SetBackgroundImageTempData();
        }

        private readonly UsersDAO _dataAccess;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About the Photographer.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Sydney to schedule a session!";

            return View();
        }


       
    }
}