using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ControleDApi.App_Start
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public ClaimsAuthorizeAttribute() { }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
           
            if (user.IsInRole("Medico"))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }

}