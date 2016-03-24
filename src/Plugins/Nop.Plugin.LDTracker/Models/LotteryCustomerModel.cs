using Nop.Core;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.LDTracker.Models
{
    public partial class LotteryCustomerModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.FullName")]
        public string FullName { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.MobilePhone")]
        public string MobilePhone { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.Note")]
        public string Note { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.Address")]
        public string Address { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.Active")]
        public bool Active { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.Deleted")]
        public bool Deleted { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.CreateDate")]
        public DateTime CreateDate { get; set; }

        #region CustomerPrice

        public decimal Lo { get; set; }

        public decimal De { get; set; }

        public decimal Xien { get; set; }

        #endregion CustomerPrice
    }
}
