using IMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace IMS.Models.CBL
{
    public class SessionAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            try
            {
                Authenticate ObjAuthenticate = (Authenticate)filterContext.HttpContext.Session["SYSSOFTECHSession"];
                if (ObjAuthenticate != null)
                {
                    if ((Convert.ToInt32(ObjAuthenticate.UserId) <= 0))
                    {
                        //List<Menu_Master_Display> objMenu_Master_Display = 
                        filterContext.Result = new HttpUnauthorizedResult();
                    }
                    else
                    {
                        try
                        {
                            ObjAuthenticate.ObjMenu_Master_Role_Wise = (List<Menu_Master_Role_Wise>)filterContext.HttpContext.Session["Menu_Master_Role_Wise"];
                            string AppToken = (filterContext.HttpContext.Request.QueryString["AppToken"] == null ? filterContext.HttpContext.Request.Form["AppToken"] : filterContext.HttpContext.Request.QueryString["AppToken"]).Replace(' ','+');
                            int MenuId = CommonUtility.GetMenuID(AppToken);
                            Menu_Master_Role_Wise obj = ObjAuthenticate.ObjMenu_Master_Role_Wise.Find(X => X.MenuID == MenuId);
                            if (obj == null)
                            {
                                filterContext.Result = new HttpUnauthorizedResult();
                            }
                        }
                        catch (Exception ex)
                        {
                            filterContext.Result = new HttpUnauthorizedResult();
                        }

                    }
                }
                else
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //Redirecting the user to the Login View of Account Controller  
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "controller", "Home" },
                     { "action", "Index" }
                });
            }
        }
    }
}