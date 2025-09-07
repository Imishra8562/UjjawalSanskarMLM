using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("tbl_User_Donation")]
    public class User_Donation : Base
    {
        [Identifier("User_Donation_Id")]
        public int User_Donation_Id { get; set; }
        public string Full_Name { get; set; }
        public string PhoneNo { get; set; }
        public decimal DonationAmount { get; set; }
        public string UTR_No { get; set; }
        public DateTime DonationDate { get; set; }
        public string PaymentSS { get; set; }
        public string Note { get; set; }
    }
}
