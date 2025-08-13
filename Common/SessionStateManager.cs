using System.Collections.Generic;
using System.Web;

namespace Common
{
    //public class SessionStateManager
    //{

    //    private static object GetSessionVariable(string key, object defaultValue)
    //    {
    //        if (HttpContext.Current.Session[key] == null)
    //        {
    //            return defaultValue;
    //        }
    //        else
    //        {
    //            return HttpContext.Current.Session[key];
    //        }
    //    }

    //    private static void ResetSessionVariable(string key, object value)
    //    {
    //        if (HttpContext.Current.Session[key] != null)
    //        {
    //            HttpContext.Current.Session.Remove(key);
    //        }
    //        HttpContext.Current.Session.Add(key, value);
    //    }

    //    public static void ClearSessionState()
    //    {
    //        HttpContext.Current.Session.Clear();
    //    }

    //    public static int? Logged_User_Id
    //    {
    //        get
    //        {
    //            return (int?)GetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_ID, null);
    //        }
    //        set
    //        {
    //            ResetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_ID, value);
    //        }
    //    }

    //    public static string Logged_User_Name
    //    {
    //        get
    //        {
    //            return (string)GetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_NAME, null);
    //        }
    //        set
    //        {
    //            ResetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_NAME, value);
    //        }
    //    }

    //    public static string Logged_Email_Id
    //    {
    //        get
    //        {
    //            return (string)GetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_EMAIL_ID, null);
    //        }
    //        set
    //        {
    //            ResetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_EMAIL_ID, value);
    //        }
    //    }

    //    public static int? Logged_User_Role_Id
    //    {
    //        get
    //        {
    //            return (int?)GetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_ROLE_ID, null);
    //        }
    //        set
    //        {
    //            ResetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_ROLE_ID, value);
    //        }
    //    }

    //    public static string Logged_User_Role_Name
    //    {
    //        get
    //        {
    //            return (string)GetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_ROLE_NAME, null);
    //        }
    //        set
    //        {
    //            ResetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_ROLE_NAME, value);
    //        }
    //    }

    //    public static IList<int> Logged_User_Permission
    //    {
    //        get
    //        {
    //            return (IList<int>)GetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_PERMISSION, null);
    //        }
    //        set
    //        {
    //            ResetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_USER_PERMISSION, value);
    //        }
    //    }

    //    public static int? Logged_Candidate_Profile_Id
    //    {
    //        get
    //        {
    //            return (int?)GetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_CANDIDATE_PROFILE_ID, null);
    //        }
    //        set
    //        {
    //            ResetSessionVariable(HttpSessionVarConstant.HTTP_SESSION_LOGGED_CANDIDATE_PROFILE_ID, value);
    //        }
    //    }

    //}

}
