using System;

namespace Domain
{
    [Table("tbl_Email_OTP")]
    public class Email_OTP : Base
    {
        [Identifier("Email_OTP_Id")]
        public int Email_OTP_Id { get; set; }  
        public string Email_Id { get; set; }
        public string OTP { get; set; }
        public string Note { get; set; }
    }
}
