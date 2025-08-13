using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Admin_DashBoard
    {
        public string User_Id { get; set; }
        public decimal Given_Help { get; set; }
        public decimal Reacived_Help { get; set; }
        public decimal Community { get; set; }
        public decimal Direct_Sponser { get; set; }
        public decimal Pending_Reward { get; set; }
        public decimal Disbursed_Reward { get; set; }
        public decimal Delevier_Token_Id { get; set; }
        public decimal Pending_Token_Id { get; set; }
    }
}
