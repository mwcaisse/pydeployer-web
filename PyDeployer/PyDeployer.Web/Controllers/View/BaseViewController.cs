using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PyDeployer.Web.Configuration;

namespace PyDeployer.Web.Controllers.View
{
    public class BaseViewController : Controller
    {

        protected readonly ApplicationConfiguration ApplicationConfiguration;
        //protected readonly BuildInformation BuildInformation;

        public BaseViewController(ApplicationConfiguration applicationConfiguration)
        {
            this.ApplicationConfiguration = applicationConfiguration;
            //this.BuildInformation = buildInformation;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.IsAuthenticated = IsAuthenticated();
            ViewBag.RootPathPrefix = ApplicationConfiguration.RootPathPrefix;

            //ViewBag.BuildInformation = BuildInformation;
        }

        protected bool IsAuthenticated()
        {
            return null != User && User.Identity.IsAuthenticated;
        }

        protected bool ContainsUrlParameter(string parameterName)
        {
            return Request.Query.ContainsKey(parameterName);
        }
    }
}
