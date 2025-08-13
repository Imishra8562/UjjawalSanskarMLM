using System.Web;
using System.Web.Security;

namespace Common
{

    public class AuthenticationManager
    {

        public static void SignOutCurrentUser()
        {
            //SessionStateManager.ClearSessionState();
            CookiesStateManager.ClearCookiesState();
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }
    }
}
