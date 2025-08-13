using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("tbl_Status")]
    public class Status:Base
    {
        [Identifier("Status_Id")]
        public int Status_Id { get; set; }
        public string Status_Name { get; set; }
    }
}
