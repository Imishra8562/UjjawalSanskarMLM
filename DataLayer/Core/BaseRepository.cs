using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Domain;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataLayer
{
    public class BaseRepository<T>
    {
        public string TableName { get; set; }

        public BaseRepository()
        {
            Type entityType = typeof(T);

            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(entityType);

            foreach (System.Attribute attr in attrs)
            {
                if (attr is TableAttribute)
                {
                    TableAttribute table = (TableAttribute)attr;
                    TableName = table.Name;
                    break;
                }
            }

        }

        public int Add(T entity)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database _db = factory.Create("DefConn");
            DbConnection con = _db.CreateConnection();

            try
            {
                string Procedure_Name = DataBaseUtil.GetProcedureName(TableName.Replace("tbl_", ""), ProcedureType.sp_Add_);
                DbCommand sqlCommand = _db.GetStoredProcCommand(Procedure_Name);
                sqlCommand.Connection = con;

                PropertyInfo IdProperty = DataBaseUtil.GetIdProperty<T>();

                IList<PropertyInfo> properties = DataBaseUtil.GetPropertiesForType<T>();

                foreach (PropertyInfo property in properties)
                {
                    if (property != IdProperty)
                    {
                        var parameter = sqlCommand.CreateParameter();
                        parameter.ParameterName = "@" + property.Name;
                        parameter.Value = property.GetValue(entity, null);
                        sqlCommand.Parameters.Add(parameter);
                    }
                }

                con.Open();
                object result = sqlCommand.ExecuteScalar();
                con.Close();

                int str = 0;
                if (result != null) { str = Convert.ToInt32(result); }

                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public DataSet List(int? Id)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database _db = factory.Create("DefConn");
            DbConnection con = _db.CreateConnection();

            try
            {
                string Procedure_Name = DataBaseUtil.GetProcedureName(TableName.Replace("tbl_", ""), ProcedureType.sp_List_);
                DbCommand sqlCommand = _db.GetStoredProcCommand(Procedure_Name);
                sqlCommand.Connection = con;

                PropertyInfo IdProperty = DataBaseUtil.GetIdProperty<T>();

                var parameter = sqlCommand.CreateParameter();
                parameter.ParameterName = IdProperty.Name;
                parameter.Value = Id;
                sqlCommand.Parameters.Add(parameter);

                con.Open();
                DataSet dataSet = new DataSet();
                _db.LoadDataSet(sqlCommand, dataSet, TableName);
                con.Close();

                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public int Update(T entity)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database _db = factory.Create("DefConn");
            DbConnection con = _db.CreateConnection();

            try
            {
                string Procedure_Name = DataBaseUtil.GetProcedureName(TableName.Replace("tbl_", ""), ProcedureType.sp_Update_);
                DbCommand sqlCommand = _db.GetStoredProcCommand(Procedure_Name);
                sqlCommand.Connection = con;

                IList<PropertyInfo> properties = DataBaseUtil.GetPropertiesForType<T>();

                foreach (PropertyInfo property in properties)
                {
                    var parameter = sqlCommand.CreateParameter();
                    parameter.ParameterName = property.Name;
                    parameter.Value = property.GetValue(entity, null);
                    sqlCommand.Parameters.Add(parameter);
                }

                con.Open();
                object result = sqlCommand.ExecuteScalar();
                con.Close();

                int str = 0;
                if (result != null) { str = Convert.ToInt32(result); }

                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public int Delete(int Id)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database _db = factory.Create("DefConn");
            DbConnection con = _db.CreateConnection();

            try
            {
                string Procedure_Name = DataBaseUtil.GetProcedureName(TableName.Replace("tbl_", ""), ProcedureType.sp_Delete_);
                DbCommand sqlCommand = _db.GetStoredProcCommand(Procedure_Name);
                sqlCommand.Connection = con;

                PropertyInfo IdProperty = DataBaseUtil.GetIdProperty<T>();

                var parameter = sqlCommand.CreateParameter();
                parameter.ParameterName = IdProperty.Name;
                parameter.Value = Id;
                sqlCommand.Parameters.Add(parameter);

                con.Open();
                object result = sqlCommand.ExecuteNonQuery();
                con.Close();

                int str = 0;
                if (result != null) { str = Convert.ToInt32(result); }

                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public DataSet ListBusiness(SqlParameter[] sqlParameters)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database _db = factory.Create("DefConn");
            DbConnection con = _db.CreateConnection();

            try
            {
                string Procedure_Name = DataBaseUtil.GetProcedureName(TableName.Replace("tbl_", ""), ProcedureType.sp_List_);
                DbCommand sqlCommand = _db.GetStoredProcCommand(Procedure_Name);
                sqlCommand.Connection = con;

                PropertyInfo IdProperty = DataBaseUtil.GetIdProperty<T>();

                foreach (var items in sqlParameters)
                {
                    var parameter = sqlCommand.CreateParameter();
                    parameter.ParameterName = items.ParameterName;
                    parameter.Value = items.Value;
                    sqlCommand.Parameters.Add(parameter);
                }

                con.Open();
                DataSet dataSet = new DataSet();
                _db.LoadDataSet(sqlCommand, dataSet, TableName);
                con.Close();

                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
    }

    public class CustomBaseRepository
    {
        public DataSet ListSQLCMD(string SQL, SqlParameter[] sqlParameters)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database _db = factory.Create("DefConn");
            DbConnection con = _db.CreateConnection();

            try
            {
                DbCommand sqlCommand = new SqlCommand(SQL);
                sqlCommand.Connection = con;

                foreach (var items in sqlParameters)
                {
                    var parameter = sqlCommand.CreateParameter();
                    parameter.ParameterName = items.ParameterName;
                    parameter.Value = items.Value;
                    sqlCommand.Parameters.Add(parameter);
                }

                con.Open();
                DataSet dataSet = new DataSet();
                _db.LoadDataSet(sqlCommand, dataSet, "ABCD");
                con.Close();

                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public DataSet ListSQLSP(string Procedure_Name, SqlParameter[] sqlParameters)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database _db = factory.Create("DefConn");
            DbConnection con = _db.CreateConnection();

            try
            {
                DbCommand sqlCommand = _db.GetStoredProcCommand(Procedure_Name);
                sqlCommand.Connection = con;

                foreach (var items in sqlParameters)
                {
                    var parameter = sqlCommand.CreateParameter();
                    parameter.ParameterName = items.ParameterName;
                    parameter.Value = items.Value;
                    sqlCommand.Parameters.Add(parameter);
                }

                con.Open();
                DataSet dataSet = new DataSet();
                _db.LoadDataSet(sqlCommand, dataSet, "ABCD");
                con.Close();

                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

    }
}
