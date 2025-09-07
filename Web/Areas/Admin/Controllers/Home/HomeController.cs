using BusinessLayer;
using BusinessLayer.Interface;
using Common;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Admin.Controllers.Home
{
    public class HomeController : Controller
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [CookiesExpireFilter]
        public ActionResult DashBoard()
        {
            HomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();
            Model.Registration_Business_Obj = homeManager.GetRegistration(0, Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id), 0, null, null, null, null, null).FirstOrDefault();
            Model.List_Registration_Businesses_Obj = homeManager.GetMyTeam(CookiesStateManager.Cookies_Logged_Token_Id);
            return View(Model);
        }
        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }
        [CookiesExpireFilter]
        public ActionResult Header()
        {
            IHomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();
            Model.Registration_Business_Obj = homeManager.GetRegistration(0, 0, 0,CookiesStateManager.Cookies_Logged_Token_Id, null, null, null, null).FirstOrDefault();
            return PartialView("_Header", Model);
        }
        public ActionResult Footer()
        {
            return PartialView("_Footer");
        }

        [CookiesExpireFilter]
        public ActionResult WelcomeLetter()
        {
            IHomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();

            Model.Registration_Business_Obj = homeManager.GetRegistration(0, 0, 0, CookiesStateManager.Cookies_Logged_Token_Id, null, null, null, null).FirstOrDefault();

            return View(Model);
        }

        [CookiesExpireFilter]
        public ActionResult IDCard()
        {
            IHomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();

            Model.Registration_Business_Obj = homeManager.GetRegistration(0, 0,0, CookiesStateManager.Cookies_Logged_Token_Id, null, null, null, null).FirstOrDefault();

            return View(Model);
        }
        [CookiesExpireFilter]
        public ActionResult Certificate(string Token_Id)
        {
            if (String.IsNullOrEmpty(Token_Id))
                Token_Id = CookiesStateManager.Cookies_Logged_Token_Id;

            IHomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();
            Model.Registration_Business_Obj = homeManager.GetRegistration(0, 0, 0, Token_Id, null, null, null, null).FirstOrDefault();
            return View(Model);
        }   

        #region Email OTP
        [CookiesExpireFilterAttribute]
        public ActionResult EmailOTP(string Email_Id)
        {
            HomeModel Model = new HomeModel();
            IHomeManager HomeManagerObj = new HomeManager();

            if (Email_Id != null && Email_Id != "")
            {
                Model.List_Email_OTP_Obj = HomeManagerObj.GetEmailOTP(0, Email_Id);
            }

            return View(Model);
        }

        public JsonResult SendEmailOTP(string Email_Id)
        {
            string return_str = "";

            ISecurityManager SecurityManagerObj = new SecurityManager();
            User_Business User_Business_Obj = SecurityManagerObj.GetUser(0, 0, Email_Id, null).FirstOrDefault();

            if (User_Business_Obj == null)
            {
                Random rnd = new Random();
                int E_OTP_C = rnd.Next(100000, 999999);
                int E_OTP_IC = rnd.Next(100000, 999999);

                CookiesStateManager.Cookies_Email_OTP = E_OTP_IC.ToString();
                string Session_Hex_Id = RandomString(7) + E_OTP_C + RandomString(7);
                string Session_Transfer_Id = RandomString(10) + E_OTP_IC + RandomString(10);

                CookiesStateManager.Cookies_Session_Hex_Id = Common.EncryptionEngine.Base64Encode(Session_Hex_Id);
                CookiesStateManager.Cookies_Session_Transfer_Id = Common.EncryptionEngine.Base64Encode(Session_Transfer_Id);

                string From = "noreply@sbcreationgroup.com";
                string To = Email_Id.ToString();
                string Subject = "MediHub India :: Email Verification !";

                string Body = new HomeController().ConvertViewToString(ControllerContext, "EmailOTP", E_OTP_C);

                new HomeController().SendMail(From, To, Subject, Body);

                IHomeManager HomeManagerObj = new HomeManager();
                Email_OTP Email_OTP_Obj = new Email_OTP();
                Email_OTP_Obj.Email_Id = Email_Id;
                Email_OTP_Obj.OTP = E_OTP_C.ToString();
                Email_OTP_Obj.Created_By = 1;
                string Ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(Ipaddress))
                {
                    Ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                }
                Email_OTP_Obj.Created_IP = Ipaddress;

                HomeManagerObj.SaveEmailOTP(Email_OTP_Obj);

                return_str = "otp";
            }
            else
            {
                return_str = "email";
            }
            return Json(return_str, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateEmailOTP(string OTP)
        {
            string Session_Hex_Id = Common.EncryptionEngine.Base64Decode(CookiesStateManager.Cookies_Session_Hex_Id);
            string E_OTP_C = Session_Hex_Id.Substring(7, 6);

            if (OTP == E_OTP_C)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Get View Page to String
        public string ConvertViewToString(ControllerContext context, string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(context, viewName);
                ViewContext vContext = new ViewContext(context, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        #endregion

        #region Send Mail

        public void SendMail(string FROM, string To, string Subject, string Body)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            MailMessage message = new MailMessage(FROM, To);

            message.Subject = Subject;
            message.Body = Body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            NetworkCredential basicCredential1 = new NetworkCredential("noreply@sbcreationgroup.com", "Sbcreation@1");
            client.EnableSsl = true;
            client.Credentials = basicCredential1;

            client.Send(message);

        }

        #endregion

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

        #region Profile
        [CookiesExpireFilter]
        public ActionResult UserProfile()
        {
            HomeModel Model = new HomeModel();
            IHomeManager homeManager = new HomeManager();
            IMasterManager masterManager = new MasterManager();
            Model.Registration_Business_Obj = homeManager.GetRegistration(0, 0, 0, CookiesStateManager.Cookies_Logged_Token_Id, null, null, null, null).FirstOrDefault();
            Model.Registration_Obj = homeManager.GetRegistration(0, 0, 0, CookiesStateManager.Cookies_Logged_Token_Id, null, null, null, null).FirstOrDefault();
            return View(Model);
        }
        [CookiesExpireFilter]
        public ActionResult UpdateRegistration(HomeModel Model)
        {
            IHomeManager homeManager = new HomeManager();
            int No = 0;
            if (Model.QRCode != null)
            {
                string fullPath = Request.MapPath("/Upload/Member/QrCode/");
                string[] files = System.IO.Directory.GetFiles(fullPath, (Model.Registration_Obj.Token_Id + "*"));
                foreach (string f in files)
                {
                    No += 1;
                }
                string extension = System.IO.Path.GetExtension(Model.QRCode.FileName);
                Model.QRCode.SaveAs(Server.MapPath("~/Upload/Member/QrCode/" + Model.Registration_Obj.Token_Id + "_" + No + extension));
                string FilePathForPhoto = "~/Upload/Member/QrCode/" + Model.Registration_Obj.Token_Id + "_" + No + extension;
                Model.Registration_Obj.UPI_QR_Code = FilePathForPhoto;
            }
            No = 0;
            if (Model.ImageFile != null)
            {
                string fullPath = Request.MapPath("/Upload/Member/ProfileImage/");
                string[] files = System.IO.Directory.GetFiles(fullPath, (Model.Registration_Obj.Token_Id + "*"));
                foreach (string f in files)
                {
                    No += 1;
                }
                string extension = System.IO.Path.GetExtension(Model.ImageFile.FileName);
                Model.ImageFile.SaveAs(Server.MapPath("~/Upload/Member/ProfileImage/" + Model.Registration_Obj.Token_Id + "_" + No + extension));
                string FilePathForPhoto = "~/Upload/Member/ProfileImage/" + Model.Registration_Obj.Token_Id + "_" + No + extension;
                Model.Registration_Obj.Img_File = FilePathForPhoto;
            }
            Model.Registration_Obj.Modified_By = Int32.Parse(CookiesStateManager.Cookies_Logged_User_Id);
            Model.Registration_Obj.Modified_IP = SystemIP();
            Model.Registration_Obj.Modified_On = DateTime.Now;
            int Id = homeManager.UpdateRegistration(Model.Registration_Obj);
            if (Id != 0 && Id > 0)
            {
                TempData["AlertType"] = "SUCCESS";
                TempData["AlertMessage"] = "Profile updated Successfully !";
            }
            else
            {
                TempData["AlertType"] = "ERROR";
                TempData["AlertMessage"] = "Sorry, Failed to Update Profile !";
            }
            return RedirectToAction("UserProfile");
        }
        #endregion

        #region User Donation
        public ActionResult UserDonation()
        {
            HomeModel homeModel = new HomeModel();
            HomeManager homeManager=new HomeManager();
            homeModel.List_User_Donation_Obj = homeManager.GetUserDonation(null);
            return View(homeModel);
        }
        #endregion
    }
}