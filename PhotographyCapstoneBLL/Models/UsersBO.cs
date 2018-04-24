using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyCapstoneBLL.Models
{
    public class UsersBO
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


        public UsersBO()
        {

        }
    }
}
