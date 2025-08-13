using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Registration_Business:Registration
    {
        public bool Is_Locked { get; set; }
        public string Status_Name { get; set; }
        public string User_Role_Name { get; set; }
        public int? TeamLevel { get; set; }
        public bool? IsGoleComplete { get; set; }
    }
}
