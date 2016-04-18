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

        #region Nested Classes

        public partial class LotteryCustomerPriceModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("LDTracker.LotteryCustomer.Fields.Name")]
            public string CategoryName { get; set; }

            public int CategoryId { get; set; }

            public int CustomerId { get; set; }

            [NopResourceDisplayName("LDTracker.LotteryCustomer.Price.Fields.Price")]
            public int Price { get; set; }

            [NopResourceDisplayName("LDTracker.LotteryCustomer.Price.Fields.WinningUnit")]
            public int WinningUnit { get; set; }

            [NopResourceDisplayName("LDTracker.LotteryCustomer.Price.Fields.Active")]
            public bool Active { get; set; }

            [NopResourceDisplayName("LDTracker.LotteryCustomer.Price.Fields.BeginUsedDate")]
            public DateTime BeginUsedDate { get; set; }
        }

        #endregion Nested Classes

        #region CustomerPrice

        public int Lo { get; set; }

        public int De { get; set; }

        public int Xien { get; set; }

        #endregion CustomerPrice
    }
}
