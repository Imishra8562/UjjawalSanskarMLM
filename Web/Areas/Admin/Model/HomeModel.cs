using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Admin
{
    public class HomeModel
    {
        #region Email Otp
        public Email_OTP Email_OTP_Obj { get; set; }
        public IList<Email_OTP> List_Email_OTP_Obj { get; set; }
        #endregion

        #region Registration
        public Registration Registration_Obj { get; set; }
        public Registration_Business Registration_Business_Obj { get; set; }
        public IList<Registration_Business> List_Registration_Businesses_Obj { get; set; }
        #endregion

        #region User
        public User User_Obj { get; set; }
        public User_Business User_Business_Obj { get; set; }
        public IList<User_Business> List_User_Businesses_Obj { get; set; }
        #endregion
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase QRCode { get; set; }
    }
}