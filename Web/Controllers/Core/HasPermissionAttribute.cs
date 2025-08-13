//using System;
//using System.Web;
//using System.Web.Mvc;
//using Domain;
//using Common;

//namespace Web.Controllers
//{
//    public class HasPermissionAttribute : ActionFilterAttribute
//    {
//        private string _submenu;
//        private string _action;

//        public HasPermissionAttribute(string SubMenu, string Action)
//        {
//            this._submenu = SubMenu;
//            this._action = Action;
//        }

//        //public override void OnActionExecuting(ActionExecutingContext filterContext)
//        //{
//        //    if (!AuthorizationManager.IsUserHasActionPermission(_submenu,_action))
//        //    {
//        //        // If this user does not have the required permission then redirect to login page
//        //        var url = new UrlHelper(filterContext.RequestContext);
//        //        var loginUrl = url.Content("~/Home/Home");
//        //        filterContext.HttpContext.Response.Redirect(loginUrl, true);
//        //    }
//        //}
//    }
//}
