using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.LDTracker.Data;
using Nop.Core.Data;
using Nop.Plugin.LDTracker.Domain;
using System.Web.Routing;

namespace Nop.Plugin.LDTracker
{
    public class LDTrackerPlugin : BasePlugin, IAdminMenuPlugin
    {
        private LDTrackerObjectContext _context;
        //private IRepository<LotteryCategory> _lotteryCategory;

        public LDTrackerPlugin(LDTrackerObjectContext context, IRepository<LotteryCategory> lotteryCategory) {
            _context = context;
            //_lotteryCategory = lotteryCategory;
        }
        public void ManageSiteMap(SiteMapNode rootNode)
        {
            List<SiteMapNode> list = new List<SiteMapNode>();
            SiteMapNode menuItem = new SiteMapNode
            {
                SystemName = "LotteryCategories",
                Title = "Loại số",
                ControllerName = "LotteryCategory",
                ActionName = "List",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } }
            };
            list.Add(menuItem);

            menuItem = new SiteMapNode
            {
                SystemName = "LotteryCustomers",
                Title = "Khách hàng",
                ControllerName = "LotteryCustomer",
                ActionName = "List",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } }
            };
            list.Add(menuItem);

            foreach (var item in list)
            {
                var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
                if (pluginNode != null)
                    pluginNode.ChildNodes.Add(item);
                else
                    rootNode.ChildNodes.Add(item);
            }
        }

        public override void Install()
        {
            _context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            _context.Uninstall();
            base.Uninstall();
        }
    }
}
