using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Areas.Admin
{
    public class MemberModel
    {
        #region Registration
        public Registration Registration_Obj { get; set; }
        public Registration_Business Registration_Business_Obj { get; set; }
        public IList<Registration_Business> List_Registration_Businesses_Obj { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        #endregion
    }
}
