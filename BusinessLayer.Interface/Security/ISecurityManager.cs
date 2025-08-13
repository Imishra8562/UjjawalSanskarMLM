using Domain;
using System.Collections.Generic;

namespace BusinessLayer.Interface
{
    public interface ISecurityManager
    {

        #region User

        int SaveUser(User Object);
        IList<User_Business> GetUser(int? User_Id, int? User_Role_Id , string Email_Id, string Mobile_No);
        int UpdateUser(User Object);
        int DeleteUser(int User_Id);

        #endregion

        #region User_Role

        int SaveUserRole(User_Role Object);
        IList<User_Role> GetUserRole(int? User_Role_Id);
        int UpdateUserRole(User_Role Object);
        int DeleteUserRole(int User_Role_Id);

        #endregion
     
        #region Login
        User_Business SignIn(string User_Name, byte[] Password);
        #endregion

    }
}
