using Nop.Core.Data;
using Nop.Plugin.LDTracker.Domain;
using Nop.Plugin.LDTracker.Models;
using Nop.Plugin.LDTracker.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Plugin.LDTracker.Extensions;
using System.Globalization;

namespace Nop.Plugin.LDTracker.Controllers
{
    [AdminAuthorize]
    public class LotteryController : BasePluginController
    {
        private readonly ILotteryFullService _lotteryService;
        private readonly ILocalizationService _localizationService;

        public LotteryController(ILotteryFullService lotteryService, ILocalizationService localizationService) {
            _lotteryService = lotteryService;
            _localizationService = localizationService;
        }

        #region Ultilities

        private string GetView(string view)
        {
            return new StringBuilder().AppendFormat("~/Plugins/LDTracker/Views/Lottery/{0}.cshtml", view).ToString();
        }

        #endregion Ultilities

        public ActionResult List()
        {
            if(DateTime.Now.TimeOfDay < System.TimeSpan.Parse("18:45:00"))
                return View(GetView("List"), new LotteryModel());

            var lottery = new LotteryModel {
                LotteryFull = _lotteryService.GetLotteryByDate(DateTime.Now.ToString("dd/MM/yyyy"))
            };
            
            return View(GetView("List"), lottery);
        }
        
        [HttpPost]
        [AdminAntiForgery]
        public ActionResult List(LotteryFull lottery, FormCollection formData)
        {
            var getByDate = Request.Form["getByDate"];
            var get24h = Request.Form["GetLotteryBy24H"];
            var getKetQuaNet = Request.Form["GetLotteryByKetQuaNet"];
            var formDate = Request.Form["DateRequest"];
            var entity = new LotteryFull();
            DateTime date;
            if(!DateTime.TryParseExact(formDate,"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return View(GetView("List"), lottery);
            }

            if (!string.IsNullOrEmpty(get24h))
                entity = _lotteryService.GetLotteryBy24H(date.ToString("dd/MM/yyyy"));

            if (!string.IsNullOrEmpty(getKetQuaNet))
                entity = _lotteryService.GetLotteryByKetQuaOrg(date.ToString("dd/MM/yyyy"));

            if (!string.IsNullOrEmpty(getByDate))
                entity = _lotteryService.GetLotteryByDate(date.ToString("dd/MM/yyyy"));

            var model = new LotteryModel
            {
                LotteryFull = entity,
                DateRequest = date
            };
            return View(GetView("List"), model);
        }
    }
}
