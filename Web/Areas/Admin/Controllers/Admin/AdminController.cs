using BusinessLayer;
using BusinessLayer.Interface;
using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        #region Get System IP
        public string SystemIP()
        {
            string Ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(Ipaddress))
            {
                Ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return Ipaddress;
        }

        #endregion

        #region User
        [CookiesExpireFilter]
        [CheckAdminUserRoleId]
        public ActionResult UserList()
        {
            IHomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();
            Model.List_Registration_Businesses_Obj = homeManager.GetRegistration(0, 0,0, null, null, null, null, null)
                .Where(u=>u.Token_Id!="US123456").ToList();
            return View(Model);
        }
        [CookiesExpireFilter]
        [CheckAdminUserRoleId]
        public ActionResult UpdateUser(int User_Id,bool Is_Locked)
        {
            ISecurityManager securityManager = new SecurityManager();
            User user=securityManager.GetUser(User_Id, 0, null, null).FirstOrDefault();
            user.Is_Locked = Is_Locked;
            user.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
            user.Modified_IP = SystemIP();
            int Id=securityManager.UpdateUser(user);
            if (Id != 0 && Id > 0)
            {
                TempData["AlertType"] = "SUCCESS";
                TempData["AlertMessage"] = "Blocking status Update Successfully !";
            }
            else
            {
                TempData["AlertType"] = "ERROR";
                TempData["AlertMessage"] = "Sorry, Failed to Update Blocking Status !";
            }
            return RedirectToAction("UserList");
        }
        #endregion
    }
}