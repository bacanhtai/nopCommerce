using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Nop.Plugin.LD.LDTracker.Infrastructure;

namespace Nop.Plugin.LD.LDTracker
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get
            {
                return 0;
            }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.MapRoute("Plugin.LD.LDTracker.ManageLDTracker", "LDTracker/List", 
                new { controler = "LDTracker", action = "List" },
                new [] { "Nop.Plugin.LD.LDTracker.Controllers" });

            //ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
