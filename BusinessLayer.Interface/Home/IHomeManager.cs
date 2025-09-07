using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer.Interface
{
    public interface IHomeManager
    {

        #region Date

        DateTime GetDateTime();

        #endregion

        #region Email_OTP

        int SaveEmailOTP(Email_OTP Object);
        IList<Email_OTP> GetEmailOTP(int? Email_OTP_Id, string Email_Id);

        #endregion

        #region Registration
        int SaveRegistration(Registration Object);
        IList<Registration_Business> GetRegistration(int? Registration_Id, int? User_Id,int? Status_Id, string Token_Id, string Sponcer_Id, string Email, string Mobile,string Position, bool Gole_Completed=false);
        IList<Registration> GetRegistrationLevelToken(string Token_Id, int? Member_Rank);
        DataTable GetRegistrationDataTable(string Token_Id,string Master_Id);
        IList<Registration_Business> GetMyTeam(string Token_Id);
        int UpdateRegistration(Registration Object);
        int DeleteRegistration(int Registration_Id);
        #endregion

        DataTable GetDashBoard(int User_Id);
        DataSet ListCustomSQLCMD(string SQL, SqlParameter[] sqlParameters);

        #region User Donation
        int SaveUserDonation(User_Donation Object);
        IList<User_Donation> GetUserDonation(int? User_Donation_Id);
        #endregion
    }
}
