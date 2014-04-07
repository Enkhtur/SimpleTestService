using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AnungooODataService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            AnungooODataService.CorsSupport.HandlePreflightRequest(HttpContext.Current);
        }

    }
}