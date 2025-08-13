using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
   public interface IMasterManager
    {
        #region Status
        int SaveStatus(Status Object);
        IList<Status> GetStatus(int? Status_Id);
        int UpdateStatus(Status Object);
        int DeleteStatus(int Status_Id);

        #endregion

    }
}
