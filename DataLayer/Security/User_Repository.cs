using Domain;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class User_Repository : BaseRepository<User>
    {
        public IList<User_Business> ListUser(int? User_Id, int? User_Role_Id, string Email_Id, string Mobile_No)
        {
            IList<User_Business> List_Obj = null;

            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_List_User");
                DataSet dataSet = new DataSet();

                SqlParameter[] sqlCommands = new SqlParameter[]
                {
                    new SqlParameter("User_Id",User_Id),
                    new SqlParameter("User_Role_Id",User_Role_Id),
                    new SqlParameter("Email_Id",Email_Id),
                    new SqlParameter("Mobile_No",Mobile_No),
                };

                sqlCommand.Parameters.AddRange(sqlCommands);

                _db.LoadDataSet(sqlCommand, dataSet, TableName);

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    List_Obj = DataBaseUtil.DataTableToList<User_Business>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return List_Obj;
        }

        public User_Business AuthenticateUser(string User_Name, byte[] Password)
        {
            User_Business User_Business_Obj = new User_Business();
            IList<User_Business> List_Obj = null;

            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_Authenticate_User");
                DataSet dataSet = new DataSet();

                var UserName = sqlCommand.CreateParameter();
                UserName.ParameterName = "User_Name";
                UserName.Value = User_Name;
                sqlCommand.Parameters.Add(UserName);

                var password = sqlCommand.CreateParameter();
                password.ParameterName = "Password";
                password.Value = Password;
                sqlCommand.Parameters.Add(password);

                sqlCommand.CommandTimeout = 600;

                _db.LoadDataSet(sqlCommand, dataSet, TableName);

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    List_Obj = DataBaseUtil.DataTableToList<User_Business>(ds.Tables[0]);
                }
                User_Business_Obj = List_Obj.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return User_Business_Obj;
        }

    }
}
