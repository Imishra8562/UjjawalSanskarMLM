using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class HomeModel
    {
        #region Registration
        public Registration Registration_Obj { get; set; }
        public Registration_Business Registration_Business_Obj { get; set; }
        public IList<Registration_Business> List_Registration_Business_Obj { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase payment_SS { get; set; }
        #endregion       

        public List<User_Donation> List_User_Donation_Obj { get; set; }
        public User_Donation User_Donation_Obj { get; set; }
        public HttpPostedFileBase PaymentSS { get; set; }
    }
}