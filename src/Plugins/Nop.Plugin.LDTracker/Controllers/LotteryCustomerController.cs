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

namespace Nop.Plugin.LDTracker.Controllers
{
    [AdminAuthorize]
    public class LotteryCustomerController : BasePluginController
    {
        private readonly ILotteryCustomerService _customerService;
        private readonly ILocalizationService _localizationService;

        public LotteryCustomerController(ILotteryCustomerService customerService, ILocalizationService localizationService) {
            _customerService = customerService;
            _localizationService = localizationService;
        }

        #region Ultilities

        private string GetView(string view)
        {
            return new StringBuilder().AppendFormat("~/Plugins/LDTracker/Views/LotteryCategory/{0}.cshtml", view).ToString();
        }

        #endregion Ultilities

        public ActionResult List()
        {
            return View(GetView("List"));
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult List(DataSourceRequest command)
        {
            var customers = _customerService.GetAllCustomers(command.Page - 1,command.PageSize);
            //var listCustomerPrice = _customerService.ge

            var gridModel = new DataSourceResult
            {
                Data = customers.Select(x =>
                {
                    var customer = x.ToModel();
                    customer.Lo = 0;
                    return customer;
                }),
                Total = customers.TotalCount
            };
            return Json(gridModel);
        }

        #region Create / Edit / Delete

        public ActionResult Create()
        {
            var category = new LotteryCategoryModel
            {
                Active = true
            };
            return View(GetView("Create"), category);
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Create(LotteryCategoryModel category, FormCollection formData)
        {
            var continueEditing = Request.Form["save-continue"];
            if (ModelState.IsValid)
            {
                var entity = category.ToEntity();
                _customerService.InsertCategory(entity);

                SuccessNotification(_localizationService.GetResource("LDTracker.LotteryCategory.Added"));
                return continueEditing != null ? RedirectToAction("Edit", new { id = category.Id }) : RedirectToAction("List");
            }

            return View("Create",category);
        }

        public ActionResult Edit(int id)
        {
            var category = _customerService.GetCategoryById(id);
            if (category == null)
                return RedirectToAction(GetView("List"));
            var model = category.ToModel();

            return View(GetView("Edit"), model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Edit(LotteryCategoryModel model, FormCollection formData)
        {
            var continueEditing = Request.Form["save-continue"];
            var category = _customerService.GetCategoryById(model.Id);
            if (category == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var entity = model.MapTo(category);
                _customerService.UpdateCategory(entity);

                SuccessNotification(_localizationService.GetResource("LDTracker.LotteryCategory.Edited"));
                return continueEditing != null ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
            }

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entity = _customerService.GetCategoryById(id);
            if (entity == null)
                //No campaign found with the specified id
                return RedirectToAction("List");

            _customerService.DeleteCategory(entity);

            SuccessNotification(_localizationService.GetResource("LDTracker.LotteryCategory.Deleted"));
            return RedirectToAction("List");
        }
        #endregion Create / Edit / Delete
    }
}
