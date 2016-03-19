using Nop.Core.Data;
using Nop.Plugin.LDTracker.Domain;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.LDTracker.Controllers
{
    public class LotteryCategoryController : BasePluginController
    {
        private IRepository<LotteryCategory> _lotteryRepository;
        
        public LotteryCategoryController(IRepository<LotteryCategory> lotteryRepository) {
            _lotteryRepository = lotteryRepository;
        }

        [AdminAuthorize]
        public ActionResult List()
        {
            return View("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");
            //return View();
        }
    }
}
