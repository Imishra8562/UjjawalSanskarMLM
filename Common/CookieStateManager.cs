using System;
using System.Collections.Generic;
using System.Web;

namespace Common
{
    public class CookiesStateManager
    {
        private static object GetCookiesVariable(string key, string defaultValue)
        {
            if (HttpContext.Current.Request.Cookies[key] == null)
            {
                return defaultValue;
            }
            else
            {
                return HttpContext.Current.Request.Cookies[key].Value;
            }
        }
        private static void ResetCookiesVariable(string key, string value)
        {
            if (HttpContext.Current.Response.Cookies[key] != null)
            {
                HttpContext.Current.Response.Cookies[key].Value = null;
            }
            HttpContext.Current.Response.Cookies[key].Value = value;
        }
        public static void ClearCookiesState()
        {
            string[] myCookies = HttpContext.Current.Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                HttpContext.Current.Request.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Request.Cookies.Remove(cookie);
            }
        }
        public static string Cookies_Logged_User_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_ID, value);
            }
        }
        public static string Cookies_Logged_User_Name
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_NAME, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_NAME, value);
            }
        }
        public static string Cookies_Logged_Email_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_EMAIL_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_EMAIL_ID, value);
            }
        }
        public static string Cookies_Logged_User_Role_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_ROLE_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_ROLE_ID, Convert.ToString(value));
            }
        }
        public static string Cookies_Logged_User_Role_Name
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_ROLE_NAME, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_USER_ROLE_NAME, value);
            }
        }
        public static string Cookies_Logged_Profile_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_PROFILE_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_PROFILE_ID, Convert.ToString(value));
            }
        }
        public static string Cookies_Logged_TPA_Profile_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_TPA_PROFILE_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_TPA_PROFILE_ID, Convert.ToString(value));
            }
        }
        public static string Cookies_Logged_TPA_User_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_TPA_USER_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_TPA_USER_ID, Convert.ToString(value));
            }
        }

        public static string Cookies_Logged_Candidate_User_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_ID, value);
            }
        }

        public static string Cookies_Logged_Candidate_User_Name
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_NAME, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_NAME, value);
            }
        }

        public static string Cookies_Logged_Candidate_Email_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_EMAIL_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_EMAIL_ID, value);
            }
        }

        public static string Cookies_Logged_Candidate_User_Role_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_ROLE_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_ROLE_ID, Convert.ToString(value));
            }
        }

        public static string Cookies_Logged_Candidate_User_Role_Name
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_ROLE_NAME, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_USER_ROLE_NAME, value);
            }
        }

        public static string Cookies_Logged_Candidate_Profile_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_PROFILE_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_CANDIDATE_PROFILE_ID, Convert.ToString(value));
            }
        }

        public static string Cookies_Email_OTP
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_EMAIL_OTP, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_EMAIL_OTP, value);
            }
        }
        public static string Cookies_Mobile_OTP
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_MOBILE_OTP, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_MOBILE_OTP, value);
            }
        }
        public static string Cookies_DateTime
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_DATETIME, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_DATETIME, value);
            }
        }
        public static string Cookies_Session_Hex_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_SESSION_HEX_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_SESSION_HEX_ID, value);
            }
        }
        public static string Cookies_Session_Transfer_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_SESSION_TRANSFER_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_SESSION_TRANSFER_ID, value);
            }
        }
        public static string Cookies_Logged_Token_Id
        {
            get
            {
                return (string)GetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_TOKEN_ID, null);
            }
            set
            {
                ResetCookiesVariable(HttpCookiesVarConstant.HTTP_COOKIES_LOGGED_TOKEN_ID, value);
            }
        }
    }
}
