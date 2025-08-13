using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Admin
{
    public class SecurityModel
    {
        #region User
        public User User_Obj { get; set; }
        public User_Business User_Business_Obj { get; set; }
        public IList<User_Business> List_User_Business_Obj { get; set; }
        public string Password { get; set; }
        #endregion

        #region User_Role
        public User_Role User_Role_Obj { get; set; }
        public IList<User_Role> List_User_Role_Obj { get; set; }
        #endregion
    }
}