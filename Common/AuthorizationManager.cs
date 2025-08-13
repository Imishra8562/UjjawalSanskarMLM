namespace Common
{
    //public static class AuthorizationManager
    //{
    //    //public static bool IsUserHasActionPermission(string SubMenu, string Action)
    //    //{
    //    //    return IsUserHasActionPermission(SubMenu, Action, SessionStateManager.UserPermission);
    //    //}
    //    //public static bool IsUserHasActionPermission(string SubMenu, string Action, IList<User_Permission_Business> UserPermissionList)
    //    //{
    //    //    bool returnValue = false;
    //    //    if (UserPermissionList == null || UserPermissionList.Count == 0)
    //    //    {
    //    //        return false;
    //    //    }

    //    //    IList<string> ListAction = new List<string>();
    //    //    if (Action.Contains(','))
    //    //    {
    //    //        ListAction = Action.Split(',').ToList();
    //    //    }
    //    //    else
    //    //    {
    //    //        ListAction.Add(Action);
    //    //    }

    //    //    foreach (var item in ListAction)
    //    //    {
    //    //        User_Permission_Business result = UserPermissionList.Where(p => p.Sub_Menu_Name.Equals(SubMenu) && p.User_Action_Name.Equals(item) && p.Is_Action_Permit.Equals(true)).FirstOrDefault();
    //    //        if (result != null)
    //    //        {
    //    //            returnValue = true;
    //    //        }
    //    //        else
    //    //        {
    //    //            returnValue = false;
    //    //            break;
    //    //        }
    //    //    }

    //    //    return returnValue;
    //    //}

    //    //public static bool IsUserHasMenuPermission(string SubMenu)
    //    //{
    //    //    return IsUserHasMenuPermission(SubMenu, SessionStateManager.UserPermission);
    //    //}
    //    //public static bool IsUserHasMenuPermission(string SubMenu, IList<User_Permission_Business> UserPermissionList)
    //    //{
    //    //    bool returnValue = false;
    //    //    if (UserPermissionList == null || UserPermissionList.Count == 0)
    //    //    {
    //    //        return false;
    //    //    }

    //    //    User_Permission_Business result = UserPermissionList.Where(p => p.Sub_Menu_Name.Equals(SubMenu) && p.Is_Permit.Equals(true)).FirstOrDefault();
    //    //    if (result != null)
    //    //    {
    //    //        returnValue = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        returnValue = false;
    //    //    }

    //    //    return returnValue;
    //    //}

    //}
}
