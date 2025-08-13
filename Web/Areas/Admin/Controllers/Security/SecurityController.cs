using BusinessLayer;
using BusinessLayer.Interface;
using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Admin.Controllers.Home;
using Web.Controllers;

namespace Web.Areas.Admin.Controllers.Security
{
    public class SecurityController : Controller
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
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            CookiesStateManager.Cookies_Logged_User_Id = null;
            CookiesStateManager.Cookies_Logged_Token_Id = null;
            CookiesStateManager.Cookies_Logged_User_Name = null;
            CookiesStateManager.Cookies_Logged_User_Role_Id = null;
            CookiesStateManager.Cookies_Logged_User_Role_Name = null;
            CookiesStateManager.Cookies_Logged_Candidate_User_Id = null;
            CookiesStateManager.Cookies_Logged_Candidate_User_Name = null;
            TempData["AlertType"] = "SUCCESS";
            TempData["AlertMessage"] = "Logout Successfully !";
            return RedirectToAction("Login","Home",new { Area=""});
        }
        public ActionResult Autheticate(string User_Name, string Password)
        {
            ISecurityManager securityManager = new SecurityManager();
            User_Business User_Business_Obj = new User_Business();
            byte[] Password1 = Encoding.ASCII.GetBytes(Password);
            User_Business_Obj = securityManager.SignIn(User_Name, Password1);
            IHomeManager Home_Manager_Obj = new HomeManager();            
            if (User_Business_Obj != null && User_Business_Obj.Is_Locked==false)
            {
                if (User_Business_Obj.FK_User_Role_Id == 3)
                {
                    Registration_Business registration_Business = new Registration_Business();
                    registration_Business = Home_Manager_Obj.GetRegistration(0, User_Business_Obj.User_Id, 0, null, null, null, null, null).FirstOrDefault();
                    if (registration_Business.FK_Reg_Status_Id == 1)
                    {
                        TempData["AlertType"] = "ERROR";
                        TempData["AlertMessage"] = "Sorry, User Request is pending please contact admin for more information!";
                        return Redirect(this.Request.UrlReferrer.AbsolutePath);
                    }
                    else if (registration_Business.FK_Reg_Status_Id == 3)
                    {
                        TempData["AlertType"] = "ERROR";
                        TempData["AlertMessage"] = "Sorry, User Request is rejected please contact admin for more information!";
                        return Redirect(this.Request.UrlReferrer.AbsolutePath);
                    }
                }
                CookiesStateManager.Cookies_Logged_User_Id = User_Business_Obj.User_Id.ToString();
                CookiesStateManager.Cookies_Logged_User_Name = User_Business_Obj.User_Name;
                CookiesStateManager.Cookies_Logged_Token_Id = User_Business_Obj.User_Name;
                CookiesStateManager.Cookies_Logged_User_Role_Id = User_Business_Obj.FK_User_Role_Id.ToString();
                CookiesStateManager.Cookies_Logged_User_Role_Name = User_Business_Obj.User_Role_Name;
                CookiesStateManager.Cookies_Logged_Candidate_User_Id = null;
                CookiesStateManager.Cookies_Logged_Candidate_User_Name = null;
                TempData["AlertType"] = "SUCCESS";
                TempData["AlertMessage"] = "Login Successfully !";
                return RedirectToAction("Dashboard","Home");
            }
            else
            {
                CookiesStateManager.Cookies_Logged_User_Id = null;
                CookiesStateManager.Cookies_Logged_Token_Id = null;
                CookiesStateManager.Cookies_Logged_User_Name = null;
                CookiesStateManager.Cookies_Logged_User_Role_Id =null;
                CookiesStateManager.Cookies_Logged_User_Role_Name = null;
                CookiesStateManager.Cookies_Logged_Candidate_User_Id = null;
                CookiesStateManager.Cookies_Logged_Candidate_User_Name = null;
                TempData["AlertType"] = "ERROR";
                TempData["AlertMessage"] = "Sorry, Invalid Credentials !";
            }

            return Redirect(this.Request.UrlReferrer.AbsolutePath);
        }

        [CookiesExpireFilter]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [CookiesExpireFilter]
        public ActionResult SaveChangePassword(string OldPassword,string NewPassword)
        {
            IHomeManager homeManager = new HomeManager();
            ISecurityManager securityManager = new SecurityManager();
            User User_Obj = securityManager.GetUser(Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id), 0, null, null).FirstOrDefault();
            Registration Registration_Obj = homeManager.GetRegistration(0, 0, 0,CookiesStateManager.Cookies_Logged_Token_Id, null, null, null, null).FirstOrDefault();
            string Password1 = Encoding.ASCII.GetString(User_Obj.Password);
            if (OldPassword==Password1)
            {
                User_Obj.Password = Encoding.ASCII.GetBytes(NewPassword);
                User_Obj.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
                User_Obj.Modified_IP = SystemIP();
                int Id = securityManager.UpdateUser(User_Obj);

                Registration_Obj.Password = NewPassword;
                Registration_Obj.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
                Registration_Obj.Modified_IP = SystemIP();
                int Id1 = homeManager.UpdateRegistration(Registration_Obj);

                if (Id > 0 && Id1 > 0)
                {
                    TempData["AlertType"] = "SUCCESS";
                    TempData["AlertMessage"] = "Password Update Successfully !";
                    return RedirectToAction("DashBoard","Home");
                }
                else
                {
                    TempData["AlertType"] = "Error";
                    TempData["AlertMessage"] = "Update to fail password !";
                }
            }
            else
            {
                TempData["AlertType"] = "SUCCESS";
                TempData["AlertMessage"] = "Wrong Password !";
            }
            return RedirectToAction("ChangePassword");
        }
        #region Forgot Password
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Forgotstep1()
        {
            return PartialView("_Forgotstep1");
        }
        public ActionResult Forgotstep2()
        {
            return PartialView("_Forgotstep2");
        }
        public ActionResult Forgotstep3()
        {
            return PartialView("_Forgotstep3");
        }
        public ActionResult ValidUser(string Email_Phone)
        {

            SecurityModel Model = new SecurityModel();
            ISecurityManager SecurityManager_Obj = new SecurityManager();
            Home.HomeController homeController = new Home.HomeController();
            Model.User_Obj = SecurityManager_Obj.GetUser(0, 0, null, Email_Phone).FirstOrDefault();
            if (Model.User_Obj == null)
            {
                Model.User_Obj = SecurityManager_Obj.GetUser(0, 0, Email_Phone, null).FirstOrDefault();
                if (Model.User_Obj == null)
                {
                    homeController.SendEmailOTP(Email_Phone);
                }
            }
            else
            {
                CookiesStateManager.Cookies_Mobile_OTP = Email_Phone;
            }
            return Json(Model.User_Obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult NewPassword(int User_Id)
        {
            SecurityModel Model = new SecurityModel();
            ISecurityManager SecurityManager_Obj = new SecurityManager();

            Model.User_Obj = SecurityManager_Obj.GetUser(User_Id, 0, null, null).FirstOrDefault();

            return PartialView("_NewPassword", Model);
        }
        public ActionResult UpdatePassword(SecurityModel Model)
        {
            ISecurityManager SecurityManager_Obj = new SecurityManager();

            User User_Obj = SecurityManager_Obj.GetUser(Model.User_Obj.User_Id, 0, null, null).FirstOrDefault();
            User_Obj.Password = Encoding.ASCII.GetBytes(Model.Password);
            User_Obj.Modified_On = DateTime.Now;
            User_Obj.Modified_By = Model.User_Obj.User_Id;

            int Id = SecurityManager_Obj.UpdateUser(User_Obj);

            if (Id != 0 && Id > 0)
            {
                TempData["DisplayMessage"] = "Changed";
            }
            else
            {
                TempData["DisplayMessage"] = "Fail";
            }

            return RedirectToAction("Login");
        }
        #endregion
    }
}