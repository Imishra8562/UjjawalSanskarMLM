using System;
using System.Web;
using System.Web.Mvc;
using Domain;
using Common;

namespace Web.Controllers
{
    //public class SessionExpireFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        HttpContext ctx = HttpContext.Current;

    //        // check if session is supported
    //        if (!SessionStateManager.Logged_User_Id.HasValue)
    //        {
    //            // check if a new session id was generated                
    //            filterContext.Result = new RedirectResult("~/Security/Login");
    //            return;
    //        }
    //        base.OnActionExecuting(filterContext);
    //    }
    //}

    public class CookiesExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.Result = new RedirectResult("~/Home/Maintenance");

            if (CookiesStateManager.Cookies_Logged_User_Id == null || CookiesStateManager.Cookies_Logged_User_Id == "")
            {
                // check if a new session id was generated                
                filterContext.Result = new RedirectResult("../../Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class CheckAdminUserRoleId : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CookiesStateManager.Cookies_Logged_User_Role_Id != "1" || CookiesStateManager.Cookies_Logged_User_Role_Id == "2")
            {
                // check if a new session id was generated                
                filterContext.Result = new RedirectResult("~/Admin/Security/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class CheckCandidateUserRoleId : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CookiesStateManager.Cookies_Logged_User_Role_Id != "3")
            {
                // check if a new session id was generated                
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class ClearCookiesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuthenticationManager.SignOutCurrentUser();
            CookiesStateManager.Cookies_Logged_User_Id = null;
            CookiesStateManager.Cookies_Logged_User_Name = null;
            CookiesStateManager.Cookies_Logged_Email_Id = null;
            CookiesStateManager.Cookies_Logged_User_Role_Id = null;
            CookiesStateManager.Cookies_Logged_User_Role_Name = null;
            CookiesStateManager.Cookies_Logged_Profile_Id = null;
            CookiesStateManager.Cookies_Email_OTP = null;
            CookiesStateManager.Cookies_Mobile_OTP = null;
            CookiesStateManager.Cookies_Logged_TPA_Profile_Id = null;
            CookiesStateManager.Cookies_Logged_TPA_User_Id = null;

        }
    }

    //public class CookiesExaminationExpireFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (CookiesStateManager.Cookies_Logged_Candidate_User_Id == null || CookiesStateManager.Cookies_Logged_Candidate_User_Id == "")
    //        {
    //            // check if a new session id was generated                
    //            filterContext.Result = new RedirectResult("~/Candidate/ExaminationLogin");
    //            return;
    //        }
    //        base.OnActionExecuting(filterContext);
    //    }
    //}

    //public class CookiesCandidateExpireFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (CookiesStateManager.Cookies_Logged_Candidate_User_Id == null || CookiesStateManager.Cookies_Logged_Candidate_User_Id == "")
    //        {
    //            // check if a new session id was generated                
    //            filterContext.Result = new RedirectResult("~/Candidate/CandidatePortal");
    //            return;
    //        }
    //        base.OnActionExecuting(filterContext);
    //    }
    //}

    //public class SessionCandidateExpireFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        HttpContext ctx = HttpContext.Current;

    //        // check if session is supported
    //        if (!SessionStateManager.Logged_User_Id.HasValue)
    //        {
    //            // check if a new session id was generated                
    //            filterContext.Result = new RedirectResult("~/Candidate/CandidatePortal");
    //            return;
    //        }
    //        base.OnActionExecuting(filterContext);
    //    }
    //}

}
