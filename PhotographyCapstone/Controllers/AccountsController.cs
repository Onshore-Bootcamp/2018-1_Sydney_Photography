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
    public class AccountsController : CustomController
    {
        public AccountsController()
        {
            string connection = ConfigurationManager.ConnectionStrings["dataSource"].ConnectionString;
            _dataAccess = new UsersDAO(connection);
            SetBackgroundImageTempData();
        }

        private readonly UsersDAO _dataAccess;
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            ActionResult response = null;
            if (Session["RoleID"] == null)
            {
                response = View();
            }
            else
            {
                TempData["Statement"] = "Opps! Looks like you're already logged in!";
                response = RedirectToAction("Index", "Home");
            }
            return response;

        }

        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            ActionResult response = null;
            if (Session["RoleID"] == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        UsersDO registeredUser = _dataAccess.ReadUser(form.UserName);
                        if (form.UserName == registeredUser.UserName.Trim() && form.Password == registeredUser.Password.Trim())
                        {
                            Session["Users"] = registeredUser.UserName;
                            Session["RoleID"] = registeredUser.RoleID;
                            Session.Timeout = 5;
                            response = RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            response = View();
                        }
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
                TempData["Statement"] = "Opps! Looks like you're already logged in!";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            ActionResult response;
            if (Session["RoleID"] == null)
            {
                response = View();
            }
            else
            {
                TempData["Statement"] = "Looks like you're already registered! If you wish to register for another user please log out and begin!";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        [HttpPost]
        public ActionResult RegisterUser(UsersPO form)
        {
            ActionResult response = null;
            if (Session["RoleID"] == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        form.RoleID = 1;
                        _dataAccess.AddUser(UserMapper.UsersPOtoDO(form));
                        response = RedirectToAction("Index", "Home");
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
                TempData["Statement"] = "Looks like you're already registered! If you wish to register for another user please log out and begin!";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        public List<SelectListItem> FillRoleID(UsersPO updateUsers)
        {
            updateUsers.RoleIDList = new List<SelectListItem>();
            updateUsers.RoleIDList.Add(new SelectListItem { Text = "User", Value = "1" });
            updateUsers.RoleIDList.Add(new SelectListItem { Text = "PowerUser", Value = "2" });
            updateUsers.RoleIDList.Add(new SelectListItem { Text = "Admin", Value = "3" });

            return updateUsers.RoleIDList;
        }

        [HttpGet]
        public ActionResult UpdateUser(int userID)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {

                try
                {
                    UsersPO userPO = UserMapper.UsersDOtoPO(_dataAccess.ReadUserByID(userID));
                    FillRoleID(userPO);
                    response = View(userPO);

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
        public ActionResult UpdateUser(UsersPO form)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                if (ModelState.IsValid)
                {
                    try
                    {


                        _dataAccess.UpdateUser(UserMapper.UsersPOtoDO(form));
                        response = RedirectToAction("ViewUsers", "Accounts");
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

                    FillRoleID(form);
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
        public ActionResult ViewUsers()
        {
            ActionResult response;

            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {
                List<UsersDO> usersList = new List<UsersDO>();
                try
                {
                    usersList = _dataAccess.ReadALLUsers();
                }
                catch (Exception ex)
                {
                    LogFile.DataFile(ex: ex);
                }
                finally
                {

                }
                response = View(UserMapper.ListUsersDOtoPO(usersList));
            }
            else
            {
                TempData["Statement"] = "Please contact Admin to gain permissions to the page you are requesting.";
                response = RedirectToAction("Index", "Home");
            }
            return response;
        }

        [HttpGet]
        public ActionResult RemoveUser(long userID)
        {
            ActionResult response = null;
            if (Session["RoleID"] != null && (byte)Session["RoleID"] == 3)
            {

                try
                {
                    _dataAccess.DeleteUser(userID);
                    response = RedirectToAction("ViewUsers", "Accounts");
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

        [HttpGet]
        public ActionResult LogOut()
        {
            ActionResult response;
            if (Session["RoleID"] != null)
            {
                Session.Abandon();
                response = RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Statement"] = "You're not currently logged in.";
                response = RedirectToAction("Login", "Accounts");
            }
            return response;
        }
    }
}