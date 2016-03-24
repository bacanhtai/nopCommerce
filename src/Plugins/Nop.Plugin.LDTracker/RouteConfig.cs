using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Nop.Plugin.LDTracker.Infrastructure;

namespace Nop.Plugin.LDTracker
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
            //routes.MapRoute("Plugin.LDTracker.ManageLDTracker", "LDTracker/List", 
            //    new { controler = "LDTracker", action = "List" },
            //    new [] { "Nop.Plugin.LD.LDTracker.Controllers" });

            routes.MapRoute("Plugin.LDTracker.LotteryCategory.Create",
                 "Plugins/LDTracker/LotteryCategory/Create",
                 new { controller = "LotteryCategory", action = "Create" },
                 new[] { "Nop.Plugin.LDTracker.Controllers" }
            );

            routes.MapRoute("Plugin.LDTracker.LotteryCategory.Edit",
                 "Plugins/LDTracker/LotteryCategory/Edit",
                 new { controller = "LotteryCategory", action = "Edit" },
                 new[] { "Nop.Plugin.LDTracker.Controllers" }
            );

            //ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
