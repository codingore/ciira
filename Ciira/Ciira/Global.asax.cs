﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ciira
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ShowCustomErrorPage(Server.GetLastError());
        }

        private void ShowCustomErrorPage(Exception exception)
        {
            HttpException httpException = exception as HttpException;
            if (httpException == null)
            {
                httpException = new HttpException(500, "Internal Server Error", exception);
            }

            Response.Clear();
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Gate");
            routeData.Values.Add("fromAppErrorEvent", true);

            switch (httpException.GetHttpCode())
            {
                case 403:
                    routeData.Values.Add("action", "AccessDenied");
                    //routeData.Values.Add("action", "ServerError");
                    break;

                case 401:
                    routeData.Values.Add("action", "NotFound");
                    break;

                case 404:
                    routeData.Values.Add("action", "NotFound");
                    break;

                case 500:
                    routeData.Values.Add("action", "ServerError");
                    break;

                default:
                    routeData.Values.Add("action", "ServerError");
                    break;
            }
            routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());

            if (httpException.GetBaseException() != null)
            {
                HttpContext.Current.Session["exception"] = httpException.GetBaseException();
            }
            else
            {
                HttpContext.Current.Session["exception"] = httpException;
            }


            Server.ClearError();

            IController controller = new Ciira.Controllers.GateController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }

    }
}