using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotographyCapstone.Models;
using PhotographyCapstoneBLL.Models;
using PhotographyCapstoneDAL.Models;

namespace PhotographyCapstone.Mapping
{
    public class UserMapper
    {
        public static  UsersPO UsersDOtoPO(UsersDO from)
        {
            UsersPO to = new UsersPO();
            to.UserID = from.UserID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.City = from.City;
            to.UserName = from.UserName;
            to.Password = from.Password;
            to.RoleID = from.RoleID;
            to.RoleName = from.RoleName;

                return to;
        }

        public static  UsersDO UsersPOtoDO (UsersPO from)
        {
            UsersDO to = new UsersDO();
            to.UserID = from.UserID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.City = from.City;
            to.UserName = from.UserName;
            to.Password = from.Password;
            to.RoleID = from.RoleID;
            to.RoleName = from.RoleName;

            return to;                         
        }

        public static UsersBO UsersDOtoBO (UsersDO from)
        {
            UsersBO to = new UsersBO();
            to.UserID = from.UserID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.City = from.City;
            to.UserName = from.UserName;
            to.Password = from.Password;
            to.RoleID = from.RoleID;
            to.RoleName = from.RoleName;

            return to;
        }

        public static  UsersPO UsersBOtoPO (UsersBO from)
        {
            UsersPO to = new UsersPO();
            to.UserID = from.UserID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.City = from.City;
            to.UserName = from.UserName;
            to.Password = from.Password;
            to.RoleID = from.RoleID;
            to.RoleName = from.RoleName;

            return to;            
        }
        
        public static  List<UsersPO> ListUsersDOtoPO(List<UsersDO> from)
        {
            List<UsersPO> to = new List<UsersPO>();
            from.ForEach((usersFrom) => to.Add(UsersDOtoPO(usersFrom)));
            return to;
        }

    }
}