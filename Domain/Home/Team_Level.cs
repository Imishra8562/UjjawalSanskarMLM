using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Team_Level:Base
    {
        public int Team_Level_Id { get; set; }
        public string Parent_Token_No { get; set; }
        public string Child_Token_No { get; set; }
        public int Level { get; set; }
    }
}
