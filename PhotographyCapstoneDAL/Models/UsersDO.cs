using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyCapstoneDAL.Models
{
    public class UsersDO
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte RoleID { get; set; }
        public string RoleName { get; set; }



        public UsersDO()
        {

        }

        public UsersDO(long newUserID, string newFirstName, string newLastName, string newEmail, string newCity, string newUserName, string newPassword, byte newRoleID, string newRoleName)
        {
            UserID = newUserID;
            FirstName = newFirstName;
            LastName = newLastName;
            Email = newEmail;
            City = newCity;
            UserName = newUserName;
            Password = newPassword;
            RoleID = newRoleID;
            RoleName = newRoleName;

        }
    }




    
}
