using BusinessLayer.Interface;
using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MasterManager :IMasterManager
    { 
        #region Status
        public int SaveStatus(Status Object)
        {
            int Id = 0;

            try
            {
                Status_Repository db = new Status_Repository();
                Id = db.Add(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public IList<Status> GetStatus(int? Status_Id)
        {
            IList<Status> ListObj = new List<Status>();
            try
            {
                Status_Repository db = new Status_Repository();
                DataSet ds = db.List(Status_Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    ListObj = DataBaseUtil.DataTableToList<Status>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListObj;
        }
        public int UpdateStatus(Status Object)
        {
            int Id = 0;

            try
            {
                Status_Repository db = new Status_Repository();
                Id = db.Update(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public int DeleteStatus(int Status_Id)
        {
            int Id = 0;

            try
            {
                Status_Repository db = new Status_Repository();
                Id = db.Delete(Status_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        #endregion
    }
}
