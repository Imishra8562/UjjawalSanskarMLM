using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataLayer
{
    public class Email_OTP_Repository : BaseRepository<Email_OTP>
    {
        public IList<Email_OTP> ListEmailOTP(int? Email_OTP_Id, string Email_Id)
        {
            IList<Email_OTP> List_Obj = null;

            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_List_Email_OTP");
                DataSet dataSet = new DataSet();

                var EmailOTPId = sqlCommand.CreateParameter();
                EmailOTPId.ParameterName = "Email_OTP_Id";
                EmailOTPId.Value = Email_OTP_Id;
                sqlCommand.Parameters.Add(EmailOTPId);

                var EmailNo = sqlCommand.CreateParameter();
                EmailNo.ParameterName = "Email_Id";
                EmailNo.Value = Email_Id;
                sqlCommand.Parameters.Add(EmailNo);

                sqlCommand.CommandTimeout = 600;

                _db.LoadDataSet(sqlCommand, dataSet, TableName);

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    List_Obj = DataBaseUtil.DataTableToList<Email_OTP>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return List_Obj;
        }


    }
}
