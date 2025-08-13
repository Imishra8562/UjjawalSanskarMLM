using Domain;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataLayer
{
    public class Registration_Repository:BaseRepository<Registration>
    {
        public IList<Registration_Business> ListRegistration(int? Registration_Id,int? User_Id, int? Status_Id, string Token_Id,string Sponcer_Id, string Email, string Mobile ,string Position,bool Gole_Completed)
        {
            IList<Registration_Business> List_Obj = null;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_List_Registration");
                sqlCommand.CommandTimeout = 120;
                DataSet dataSet = new DataSet();

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("Registration_Id",Registration_Id),
                    new SqlParameter("User_Id",User_Id),
                    new SqlParameter("Status_Id",Status_Id),
                    new SqlParameter("Token_Id",Token_Id),
                    new SqlParameter("Sponcer_Id",Sponcer_Id),
                    new SqlParameter("Email",Email),
                    new SqlParameter("Mobile",Mobile),
                    new SqlParameter("Position",Position),
                    new SqlParameter("Gole_Completed",Gole_Completed),
                };
                sqlCommand.Parameters.AddRange(sqlParameters);
                
                _db.LoadDataSet(sqlCommand, dataSet, TableName);

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    List_Obj = DataBaseUtil.DataTableToList<Registration_Business>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return List_Obj;
        }

        public IList<Registration> ListRegistrationLevelToken(string Token_Id, int? Member_Rank)
        {
            IList<Registration> List_Obj = null;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_List_Reg_Level_Token");
                sqlCommand.CommandTimeout = 120;
                DataSet dataSet = new DataSet();

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("Token_Id",Token_Id),
                    new SqlParameter("Member_Rank",Member_Rank),
                };
                sqlCommand.Parameters.AddRange(sqlParameters);

                _db.LoadDataSet(sqlCommand, dataSet, TableName);

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    List_Obj = DataBaseUtil.DataTableToList<Registration>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return List_Obj;
        }

        public IList<Registration_Business> ListMyTeam(string Token_Id)
        {
            IList<Registration_Business> List_Obj = null;
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_List_My_Team");
                sqlCommand.CommandTimeout = 120;
                DataSet dataSet = new DataSet();

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("Token_Id",Token_Id),
                };
                sqlCommand.Parameters.AddRange(sqlParameters);

                _db.LoadDataSet(sqlCommand, dataSet, TableName);

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    List_Obj = DataBaseUtil.DataTableToList<Registration_Business>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return List_Obj;
        }

        public DataTable ListMyTeamDataTable(string Token_Id,string Master_Id)
        {
            DataTable dataTable = new DataTable();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_List_My_Team_Level");
                sqlCommand.CommandTimeout = 120;
                DataSet dataSet = new DataSet();

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("Token_Id",Token_Id),
                    new SqlParameter("Master_Id",Master_Id)
                };
                sqlCommand.Parameters.AddRange(sqlParameters);

                _db.LoadDataSet(sqlCommand, dataSet, TableName);

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    dataTable=ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }
    }
}
