using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClaimsTestWeb
{
    using System.Security.Claims;
    using System.Threading;
    using System.Web.Helpers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // DC - This makes the anti-forgery only require the name identifier and not the identity provider as well
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        //protected void Application_PostAuthenticateRequest()
        //{
        //    var currentPrincipal = ClaimsPrincipal.Current;
        //    var customClaimsTransformer = new CustomClaimsTransformer();
        //    var transformedClaimPrincipal = customClaimsTransformer.Authenticate(string.Empty, currentPrincipal);
        //    Thread.CurrentPrincipal = transformedClaimPrincipal;
        //    HttpContext.Current.User = transformedClaimPrincipal;
        //}
    }
}
