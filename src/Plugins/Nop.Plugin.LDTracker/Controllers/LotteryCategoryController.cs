using Nop.Core.Data;
using Nop.Plugin.LDTracker.Domain;
using Nop.Plugin.LDTracker.Services;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.LDTracker.Controllers
{
    [AdminAuthorize]
    public class LotteryCategoryController : BasePluginController
    {
        private readonly ILotteryCategoryService _categoryService;
        public LotteryCategoryController(ILotteryCategoryService categoryService) {
            _categoryService = categoryService;
        }
        
        public ActionResult List()
        {
            return View("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");
            //return View();
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult List(DataSourceRequest command)
        {
            var categories = _categoryService.GetAllCategories(command.Page - 1,command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = categories,
                Total = categories.TotalCount
            };
            return Json(gridModel);
        }

        #region Create / Edit / Delete

        public ActionResult Create()
        {
            var category = new LotteryCategory {
                Active = true
            };
            return View(category);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(LotteryCategory category, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                _categoryService.InsertCategory(category);
            }
            return RedirectToAction("List");
        }

        #endregion Create / Edit / Delete
    }
}
