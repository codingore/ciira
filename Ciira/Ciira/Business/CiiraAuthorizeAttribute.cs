using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciira.Business
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CiiraAuthorizeAttribute : AuthorizeAttribute
    {

        public Ciira.Models.UserKind[] AllowedUserKinds { get; set; }

        public CiiraAuthorizeAttribute(params Ciira.Models.UserKind[] allowedKinds)
            : base()
        {
            AllowedUserKinds = allowedKinds;

            if (AllowedUserKinds == null)
            {
                AllowedUserKinds = new Ciira.Models.UserKind[] { };
            }
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            Ciira.Models.User cu = new Ciira.Business.GateBusiness().CurrentUser();
            if (cu != null && cu.Code != null && cu.Code.Trim().Length > 0)
            {
                if (!AllowedUserKinds.Contains((Ciira.Models.UserKind)cu.Kind))
                {
                    throw new HttpException(403, "AccessDenied");
                }
            }
            else
            {
                context.RequestContext.HttpContext.Response.StatusCode = 401;
            }
        }

    }
}