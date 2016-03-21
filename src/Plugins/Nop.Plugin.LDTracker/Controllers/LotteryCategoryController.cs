using Nop.Core.Data;
using Nop.Plugin.LDTracker.Domain;
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

namespace Nop.Plugin.LDTracker.Controllers
{
    [AdminAuthorize]
    public class LotteryCategoryController : BasePluginController
    {
        private readonly ILotteryCategoryService _categoryService;
        private readonly ILocalizationService _localizationService;

        public LotteryCategoryController(ILotteryCategoryService categoryService, ILocalizationService localizationService) {
            _categoryService = categoryService;
            _localizationService = localizationService;
        }
        
        public ActionResult List()
        {
            return View("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");
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
            return View("~/Plugins/LDTracker/Views/LotteryCategory/Create.cshtml",category);
        }

        //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Create(LotteryCategory category, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                _categoryService.InsertCategory(category);
            }
            return RedirectToAction("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");
        }

        public ActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
                return RedirectToAction("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");

            return View("~/Plugins/LDTracker/Views/LotteryCategory/Edit.cshtml", category);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(LotteryCategory entity, bool continueEditing)
        {
            var category = _categoryService.GetCategoryById(entity.Id);
            if (category == null)
                return RedirectToAction("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");

            return RedirectToAction("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entity = _categoryService.GetCategoryById(id);
            if (entity == null)
                //No campaign found with the specified id
                return RedirectToAction("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");

            _categoryService.DeleteCategory(entity);

            SuccessNotification(_localizationService.GetResource("LDTracker.LotteryCategory.Deleted"));
            return RedirectToAction("~/Plugins/LDTracker/Views/LotteryCategory/List.cshtml");
        }
        #endregion Create / Edit / Delete
    }
}
