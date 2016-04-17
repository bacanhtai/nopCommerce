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
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.LDTracker.Controllers
{
    [AdminAuthorize]
    public class LotteryCustomerController : BasePluginController
    {
        private readonly ILotteryCustomerService _customerService;
        private readonly ILotteryCategoryService _categoryServices;
        private readonly ILocalizationService _localizationService;

        public LotteryCustomerController(ILotteryCustomerService customerService, ILotteryCategoryService categoryServices, ILocalizationService localizationService) {
            _customerService = customerService;
            _categoryServices = categoryServices;
            _localizationService = localizationService;
        }

        #region Ultilities

        private string GetView(string view)
        {
            return new StringBuilder().AppendFormat("~/Plugins/LDTracker/Views/LotteryCustomer/{0}.cshtml", view).ToString();
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
            var Customer = new LotteryCustomerModel
            {
                Active = true
            };
            return View(GetView("Create"), Customer);
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Create(LotteryCustomerModel Customer, FormCollection formData)
        {
            var continueEditing = Request.Form["save-continue"];
            if (ModelState.IsValid)
            {
                var entity = Customer.ToEntity();
                _customerService.InsertCustomer(entity);

                SuccessNotification(_localizationService.GetResource("LDTracker.LotteryCustomer.Added"));
                return continueEditing != null ? RedirectToAction("Edit", new { id = Customer.Id }) : RedirectToAction("List");
            }

            return View("Create",Customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return RedirectToAction(GetView("List"));
            var model = customer.ToModel();

            return View(GetView("Edit"), model);
        }

        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Edit(LotteryCustomerModel model, FormCollection formData)
        {
            var continueEditing = Request.Form["save-continue"];
            var customer = _customerService.GetCustomerById(model.Id);
            if (customer == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var entity = model.MapTo(customer);
                _customerService.UpdateCustomer(entity);

                SuccessNotification(_localizationService.GetResource("LDTracker.LotteryCustomer.Edited"));
                return continueEditing != null ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
            }

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entity = _customerService.GetCustomerById(id);
            if (entity == null)
                //No campaign found with the specified id
                return RedirectToAction("List");

            _customerService.DeleteCustomer(entity);

            SuccessNotification(_localizationService.GetResource("LDTracker.LotteryCustomer.Deleted"));
            return RedirectToAction("List");
        }
        #endregion Create / Edit / Delete

        #region Lottery Customer Price
        
        [HttpPost]
        public ActionResult CustomerPriceList(DataSourceRequest command, int customerId)
        {
            var customerPrices = _categoryServices.GetAllCustomerPriceByCustomerId(customerId);
            var customerPriceModels = customerPrices.Select(x =>
                                    new LotteryCustomerModel.LotteryCustomerPriceModel
                                    {
                                        Id = x.Id,
                                        CategoryName = _categoryServices.GetCategoryById(x.Id).Name,
                                        CategoryId = x.CategoryId,
                                        CustomerId = x.CustomerId,
                                        Price = x.Price,
                                        WinningUnit = x.WinningUnit,
                                        Active = x.Active
                                    })
                                    .ToList();

            var gridModel = new DataSourceResult
            {
                Data = customerPriceModels,
                Total = customerPriceModels.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult CustomerPriceInsert(LotteryCustomerModel.LotteryCustomerPriceModel model)
        {
            if (model.CustomerId != 0 || model.CategoryId != 0) {
                if (_categoryServices.IsExistLotteryCustomerPrice(model.CustomerId, model.CategoryId))
                {
                    var customerPrice = new LotteryCustomerPrice
                    {
                        CategoryId = model.CategoryId,
                        CustomerId = model.CustomerId,
                        Price = model.Price,
                        WinningUnit = model.WinningUnit,
                        Active = true,
                        BeginUsedDate = DateTime.Now
                    };

                    _categoryServices.InsertCustomerPrice(customerPrice);
                }
            }
            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult CustomerPriceUpdate(LotteryCustomerModel.LotteryCustomerPriceModel model)
        {
            var customerPrice = _categoryServices.GetCustomerPriceById(model.Id);
            if (customerPrice == null)
                throw new ArgumentException("No found");

            customerPrice.CategoryId = model.CategoryId;
            customerPrice.CustomerId = model.CustomerId;
            customerPrice.Price = model.Price;
            customerPrice.WinningUnit = model.WinningUnit;
            _categoryServices.UpdateCustomerPrice(customerPrice);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult CustomerPriceDelete(int id)
        {
            var customerPrice = _categoryServices.GetCustomerPriceById(id);
            if (customerPrice == null)
                throw new ArgumentException("No found");

            _categoryServices.DeleteCustomerPrice(customerPrice);

            return new NullJsonResult();
        }

        #endregion Lottery Customer Price
    }
}
