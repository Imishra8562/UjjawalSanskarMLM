using BusinessLayer;
using BusinessLayer.Interface;
using Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public class UserData
        {
            public string Token_Id { get; set; }
            public string Sponcer_Id { get; set; }
            public string Sponcer_Name { get; set; }
            public string Parent_Id { get; set; }
            public string Parent_Name { get; set; }
            public string Position { get; set; }
        }
        public class RegistationData
        {
            public string Full_Name { get; set; }
            public IList<string> Position { get; set; }
        }
        // GET: Home
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            return PartialView("_Header");
        }
        public ActionResult Footer()
        {
            return PartialView("_Footer");
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult TermAndCondition()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View(); 
        }

        #region Registration
        public ActionResult RegistrationUrl()
        {
            IHomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();
            Registration registration = new Registration();
            Model.Registration_Obj = new Registration();
            Model.Registration_Business_Obj = homeManager.GetRegistration(1, 0, 0, null, null, null, null, null).FirstOrDefault();
            Random rnd = new Random();
            int sixDigitNumber = rnd.Next(100000, 1000000); // Generates a number between 100000 and 999999
            var Tokenid = "US" + sixDigitNumber;
            registration = homeManager.GetRegistration(0, 0, 0, Tokenid, null, null, null, null).FirstOrDefault();
            while (registration != null && registration.Token_Id == Tokenid)
            {
                sixDigitNumber = rnd.Next(100000, 1000000);
                Tokenid = "US" + sixDigitNumber;
                registration = homeManager.GetRegistration(0, 0, 0, Tokenid, null, null, null, null).FirstOrDefault();
            }
            Model.Registration_Obj.Token_Id = Tokenid;
            return View(Model);
        }
        [HttpPost]
        public ActionResult SaveRegistratinon(HomeModel Model)
        {
            int No = 0;
            if (Model.payment_SS != null)
            {
                string fullPath = Request.MapPath("/Upload/Payment/Payment_Snap/");
                string[] files = System.IO.Directory.GetFiles(fullPath, (Model.Registration_Obj.Token_Id + "*"));
                foreach (string f in files)
                {
                    No += 1;
                }
                string extension = System.IO.Path.GetExtension(Model.payment_SS.FileName);
                Model.payment_SS.SaveAs(Server.MapPath("~/Upload/Payment/Payment_Snap/" + Model.Registration_Obj.Token_Id + "_" + No + extension));
                string FilePathForPhoto = "~/Upload/Payment/Payment_Snap/" + Model.Registration_Obj.Token_Id + "_" + No + extension;
                Model.Registration_Obj.Payment_SS = FilePathForPhoto;
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
            IHomeManager homeManager = new HomeManager();
            Model.Registration_Obj.Created_IP = SystemIP();
            Model.Registration_Obj.Created_By = 1;
            Model.Registration_Obj.Password = Model.Password;
            Model.Registration_Obj.FK_Reg_Status_Id = 1;
            int Id = homeManager.SaveRegistration(Model.Registration_Obj);
            if (Id != 0 && Id > 0)
            {
                // Send welcome email
                bool mailSent = false;
                try
                {
                    var reg = Model.Registration_Obj;
                    string to = reg.Email;
                    string subject = "Welcome to Ujjawal Sansakar!";
                    string body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='UTF-8'>
                        <title>Welcome to Ujjawal Sansakar</title>
                        <style>
                            body {{ font-family: Arial, sans-serif; background: #f4f4f4; margin: 0; padding: 0; }}
                            .container {{ max-width: 600px; margin: 40px auto; background: #fff; border-radius: 10px; box-shadow: 0 2px 8px #e0e0e0; padding: 30px; }}
                            .header {{ background: #4caf50; color: #fff; padding: 20px 0; border-radius: 10px 10px 0 0; text-align: center; }}
                            .content {{ padding: 20px; }}
                            .details {{ background: #f9f9f9; border-radius: 8px; padding: 15px; margin: 20px 0; }}
                            .details ul {{ list-style: none; padding: 0; }}
                            .details li {{ margin-bottom: 8px; font-size: 16px; }}
                            .footer {{ text-align: center; color: #888; font-size: 13px; margin-top: 30px; }}
                            .wait-approval {{ color: #d84315; font-weight: bold; margin-top: 20px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1>Welcome to Ujjawal Sansakar!</h1>
                            </div>
                            <div class='content'>
                                <p>Dear <b>{reg.Full_Name}</b>,</p>
                                <p>We are thrilled to have you join the <b>Ujjawal Sansakar</b> family! Your registration was <b>successful</b>.</p>
                                <div class='details'>
                                    <h3>Your Registration Details</h3>
                                    <ul>
                                        <li><b>User Id:</b> {reg.Token_Id}</li>
                                        <li><b>Password:</b> {reg.Password}</li>
                                        <li><b>Sponsor Id:</b> {reg.Sponsor_Id}</li>
                                        <li><b>Sponsor Name:</b> {reg.Sponsor_Name}</li>
                                        <li><b>Parent Id:</b> {reg.Parent_Id}</li>
                                        <li><b>Parent Name:</b> {reg.Parent_Name}</li>
                                        <li><b>Position:</b> {reg.Position}</li>
                                        <li><b>Status:</b> Pending</li>
                                    </ul>
                                </div>
                                <div class='wait-approval'>
                                    Please wait for admin approval before you can access all features.<br/>
                                    You will be notified once your account is activated.
                                </div>
                                <p>Thank you for choosing us. We wish you a wonderful journey ahead!</p>
                            </div>
                            <div class='footer'>
                                &copy; {DateTime.Now.Year} Ujjawal Sansakar. All rights reserved.
                            </div>
                        </div>
                    </body>
                    </html>
                    ";
                    mailSent = SendMail("princekumar.ifb@gmail.com", to, subject, body);
                }
                catch { /* Optionally log error */ }
               
                TempData["AlertType"] = mailSent ? "SUCCESS" : "WARNING";
                TempData["AlertMessage"] = mailSent ? "Account Created Successfully !" : "Account Created, but welcome email could not be sent.";
                return RedirectToAction("WelcomePage", new { Registration_Id = Id });
            }
            else
            {
                TempData["AlertType"] = "ERROR";
                TempData["AlertMessage"] = "Sorry, Failed to Registration !";
                string url = this.Request.UrlReferrer.AbsoluteUri;
                return Redirect(url);
            }
        }
        public bool SendMail(string FROM, string To, string Subject, string Body)
        {
            try
            {
                FROM = "info@Ujjawalsansakar.co.in";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MailMessage message = new MailMessage(FROM, To);
                message.Subject = Subject;
                message.Body = Body;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp-relay.brevo.com", 587); // Brevo SMTP
                client.UseDefaultCredentials = false;
                NetworkCredential basicCredential1 = new NetworkCredential("949c19001@smtp-brevo.com", "5R0m2TdqAw4K1JCz"); // Replace with your Brevo email and SMTP key
                client.EnableSsl = true;
                client.Credentials = basicCredential1;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine("Error sending email: " + ex.Message);
                return false;
            }
        }
        public ActionResult GetDecryptedData(string UrlData)
        {
            IHomeManager homeManager = new HomeManager();
            UserData data = new UserData();
            UrlData = EncryptionEngine.Base64Decode(UrlData);
            var data1 = UrlData.Split('&');
            data.Sponcer_Id = data1[0].Split('=')[1];
            data.Parent_Id = data1[2].Split('=')[1];
            data.Position = data1[4].Split('=')[1];
            data.Sponcer_Name = homeManager.GetRegistration(0, 0, 0, data.Sponcer_Id, null, null, null, null).FirstOrDefault().Full_Name;
            data.Parent_Name = homeManager.GetRegistration(0, 0, 0, data.Parent_Id, null, null, null, null).FirstOrDefault().Full_Name;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetName(string Token_Id)
        {
            RegistationData Data = new RegistationData();
            IHomeManager homeManager = new HomeManager();
            IList<string> str = new List<string>();
            Registration registration = homeManager.GetRegistration(0, 0, 0, Token_Id, null, null, null, null).FirstOrDefault();
            if (registration != null)
            {
                Data.Full_Name = registration.Full_Name;
                if ((homeManager.GetRegistration(0, 0, 0, null, Token_Id, null, null, "Left").FirstOrDefault()) == null)
                {
                    str.Add("Left");
                }
                if ((homeManager.GetRegistration(0, 0, 0, null, Token_Id, null, null, "Right").FirstOrDefault()) == null)
                {
                    str.Add("Right");
                }
                Data.Position = str;
            }
            return Json(Data);
        }
        public ActionResult WelcomePage(int? Registration_Id)
        {
            IHomeManager homeManager = new HomeManager();
            HomeModel Model = new HomeModel();
            Model.Registration_Obj = homeManager.GetRegistration(Registration_Id, 0, 0, null, null, null, null, null).FirstOrDefault();
            return View(Model);
        }
        #endregion
    }
}