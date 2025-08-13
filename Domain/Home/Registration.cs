using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Table("tbl_Registration")]
    public class Registration:Base
    {
        [Identifier("Registration_Id")]
        public int Registration_Id { get; set; }
        public int FK_User_Id { get; set; }
        public int FK_Reg_Status_Id { get; set; }
        public string Registration_Code { get; set; }
        public string Token_Id { get; set; }
        public string Full_Name { get; set; }
        public string Password { get; set; }
        public string Sponsor_Id { get; set; }
        public string Sponsor_Name { get; set; }
        public string Parent_Id { get; set; }
        public string Parent_Name { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }

        //Comminision
        public string Mobile { get; set; }
        public string Email { get; set; }
        //KYC
        public string PAN_No { get; set; }
        public string Aadhar_No { get; set; }
        public string Img_File { get; set; }
        public bool Is_KYC_Approved { get; set; }

        public string Bank_Name { get; set; }
        public string IFSC_Code { get; set; }
        public string Account_No { get; set; }
        public string Account_Holder_Name { get; set; }
        public string UPI_ID { get; set; }
        public string UPI_QR_Code { get; set; }

        //Payment Data
        public string Transation_Id { get; set; }
        public string Payment_SS { get; set; }
        public DateTime? Payment_Date { get; set; }
        public decimal Registration_Fee { get; set; }     
        public decimal Registration_Gst { get; set; }     
        public decimal Total_Reg_Fee_Paid { get; set; }     
        public bool IsRegFeeApproved { get; set; }       
        public bool IsPaid { get; set; }
        public DateTime? ActiveDate { get; set; }       

        public decimal Team_Business { get; set; }
    }
}