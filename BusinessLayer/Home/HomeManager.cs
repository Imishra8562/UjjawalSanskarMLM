using BusinessLayer.Interface;
using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace BusinessLayer
{
    public class HomeManager : IHomeManager
    {
        #region Date

        public DateTime GetDateTime()
        {
            DateTime DateTime_Obj = new DateTime();
            try
            {
                DateTime_Repository db = new DateTime_Repository();
                DateTime_Obj = db.ListDateTime();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DateTime_Obj;
        }

        #endregion

        #region Email_OTP

        public int SaveEmailOTP(Email_OTP Object)
        {
            int Id = 0;

            try
            {
                Email_OTP_Repository db = new Email_OTP_Repository();
                Id = db.Add(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public IList<Email_OTP> GetEmailOTP(int? Email_OTP_Id, string Email_Id)
        {
            IList<Email_OTP> ListObj = new List<Email_OTP>();
            try
            {
                Email_OTP_Repository db = new Email_OTP_Repository();
                ListObj = db.ListEmailOTP(Email_OTP_Id, Email_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListObj;
        }

        #endregion

        #region Registration
        public int SaveRegistration(Registration Object)
        {
            int Id = 0;

            try
            {
                Registration_Repository db = new Registration_Repository();
                Id = db.Add(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public IList<Registration_Business> GetRegistration(int? Registration_Id, int? User_Id, int? Status_Id, string Token_Id, string Sponcer_Id, string Email, string Mobile, string Position,bool Gole_Completed=false)
        {
            IList<Registration_Business> ListObj = new List<Registration_Business>();
            try
            {
                Registration_Repository db = new Registration_Repository();
                ListObj = db.ListRegistration(Registration_Id,User_Id, Status_Id, Token_Id,Sponcer_Id,Email,Mobile,Position, Gole_Completed);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListObj;
        }
        public IList<Registration> GetRegistrationLevelToken(string Token_Id, int? Member_Rank)
        {
            IList<Registration> ListObj = new List<Registration>();
            try
            {
                Registration_Repository db = new Registration_Repository();
                ListObj = db.ListRegistrationLevelToken(Token_Id, Member_Rank);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListObj;
        } 
        public IList<Registration_Business> GetMyTeam(string Token_Id)
        {
            IList<Registration_Business> ListObj = new List<Registration_Business>();
            try
            {
                Registration_Repository db = new Registration_Repository();
                ListObj = db.ListMyTeam(Token_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListObj;
        }
        public int UpdateRegistration(Registration Object)
        {
            int Id = 0;

            try
            {
                Registration_Repository db = new Registration_Repository();
                Id = db.Update(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public int DeleteRegistration(int Registration_Id)
        {
            int Id = 0;

            try
            {
                Registration_Repository db = new Registration_Repository();
                Id = db.Delete(Registration_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public DataTable GetRegistrationDataTable(string Token_Id, string Master_Id)
        {
            DataTable dataTable = new DataTable();
            try
            {
                Registration_Repository db = new Registration_Repository();
                dataTable = db.ListMyTeamDataTable(Token_Id,Master_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }
        #endregion  
      
        #region DashBoard
        public DataTable GetDashBoard(int User_Id)
        {
            IList<Admin_DashBoard> ListObj = new List<Admin_DashBoard>();
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("FK_User_Id",User_Id)
                };
                CustomBaseRepository db = new CustomBaseRepository();
                DataSet ds = db.ListSQLSP("sp_Get_Dashboard_Data", sqlParameters);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new DataTable();
        }
        #endregion
        public DataSet ListCustomSQLCMD(string SQL, SqlParameter[] sqlParameters)
        {
            CustomBaseRepository _db = new CustomBaseRepository();
            var data = _db.ListSQLCMD(SQL, sqlParameters);
            return data;
        }

        #region User Donation
        public int SaveUserDonation(User_Donation Object)
        {
            int Id = 0;

            try
            {
                BaseRepository<User_Donation> db = new BaseRepository<User_Donation>();
                Id = db.Add(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public IList<User_Donation> GetUserDonation(int? User_Donation_Id)
        {
            IList<User_Donation> ListObj = new List<User_Donation>();
            try
            {
                BaseRepository<User_Donation> db = new BaseRepository<User_Donation>();
                DataSet ds = db.List(User_Donation_Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    ListObj = DataBaseUtil.DataTableToList<User_Donation>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListObj;
        }
        #endregion

    }
}
