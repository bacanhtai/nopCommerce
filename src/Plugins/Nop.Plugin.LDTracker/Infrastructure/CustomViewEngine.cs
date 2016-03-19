using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.LDTracker.Infrastructure
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        public CustomViewEngine() {
            ViewLocationFormats = new[] { "~/Plugins/LDTracker/View/{0}.cshtml"};
            PartialViewLocationFormats = new[] { "~/Plugins/LDTracker/View/{0}.cshtml" };
        }
    }
}
