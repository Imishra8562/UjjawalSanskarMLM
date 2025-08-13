using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("tbl_User")]
    public class User : Base
    {
        [Identifier("User_Id")]
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string Mobile_No { get; set; }
        public string Email_Id { get; set; }
        public byte[] Password { get; set; }
        public int FK_User_Role_Id { get; set; }
        public bool Is_Locked { get; set; }
        public DateTime? Last_Login { get; set; }
    }
}
