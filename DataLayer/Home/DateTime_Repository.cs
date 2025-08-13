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
    public class DateTime_Repository : BaseRepository<DateTime>
    {
        public DateTime ListDateTime()
        {
            DateTime DateTime_Obj = new DateTime();
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database _db = factory.Create("DefConn");
                DbCommand sqlCommand = _db.GetStoredProcCommand("sp_List_DateTime");
                DataSet dataSet = new DataSet();

                sqlCommand.CommandTimeout = 600;

                _db.LoadDataSet(sqlCommand, dataSet, "DateTime");

                DataSet ds = dataSet;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    DateTime_Obj = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DateTime_Obj;
        }

    }
}
