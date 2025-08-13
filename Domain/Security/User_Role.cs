using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("tbl_User_Role")]
    public class User_Role : Base
    {
        [Identifier("User_Role_Id")]
        public int User_Role_Id { get; set; }
        public string User_Role_Name { get; set; }
        public string User_Role_Description { get; set; }
    }
}
