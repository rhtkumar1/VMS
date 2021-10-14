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
            Authenticate ObjAuthenticate = (Authenticate)filterContext.HttpContext.Session["SYSSOFTECHSession"];
            if (ObjAuthenticate != null)
            {
                if ((Convert.ToInt32(ObjAuthenticate.LoginId) <= 0))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
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