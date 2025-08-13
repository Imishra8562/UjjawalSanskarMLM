using BusinessLayer.Interface;
using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer
{
    public class SecurityManager : ISecurityManager
    {
        #region User

        public int SaveUser(User Object)
        {
            int Id = 0;

            try
            {
                User_Repository db = new User_Repository();
                Id = db.Add(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public IList<User_Business> GetUser(int? User_Id, int? User_Role_Id, string Email_Id, string Mobile_No)
        {
            IList<User_Business> ListObj = new List<User_Business>();
            try
            {
                User_Repository db = new User_Repository();
                ListObj = db.ListUser(User_Id, User_Role_Id, Email_Id, Mobile_No);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListObj;
        }
        public int UpdateUser(User Object)
        {
            int Id = 0;

            try
            {
                User_Repository db = new User_Repository();
                Id = db.Update(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public int DeleteUser(int User_Id)
        {
            int Id = 0;

            try
            {
                User_Repository db = new User_Repository();
                Id = db.Delete(User_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        #endregion

        #region User_Role

        public int SaveUserRole(User_Role Object)
        {
            int Id = 0;

            try
            {
                User_Role_Repository db = new User_Role_Repository();
                Id = db.Add(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public IList<User_Role> GetUserRole(int? User_Role_Id)
        {
            IList<User_Role> ListObj = new List<User_Role>();
            try
            {
                User_Role_Repository db = new User_Role_Repository();
                DataSet ds = db.List(User_Role_Id);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null)
                {
                    ListObj = DataBaseUtil.DataTableToList<User_Role>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ListObj;
        }
        public int UpdateUserRole(User_Role Object)
        {
            int Id = 0;

            try
            {
                User_Role_Repository db = new User_Role_Repository();
                Id = db.Update(Object);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }
        public int DeleteUserRole(int User_Role_Id)
        {
            int Id = 0;

            try
            {
                User_Role_Repository db = new User_Role_Repository();
                Id = db.Delete(User_Role_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Id;
        }

        #endregion

        #region Login

        public User_Business SignIn(string User_Name, byte[] Password)
        {
            User_Business User_Business_Obj = null;
            try
            {
                User_Repository db = new User_Repository();
                User_Business_Obj = db.AuthenticateUser(User_Name, Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return User_Business_Obj;
        }

        #endregion
    }
}
