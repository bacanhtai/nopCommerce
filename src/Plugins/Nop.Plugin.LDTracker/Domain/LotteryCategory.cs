using Nop.Core;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.LDTracker.Domain
{
    public partial class LotteryCategory : BaseEntity
    {
        [NopResourceDisplayName("LDTracker.LotteryCategory.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCategory.Fields.WinningUnit")]
        public int WinningUnit { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCategory.Fields.Price")]
        public int Price { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCategory.Fields.DuplicateWinning")]
        public bool DuplicateWinning { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCategory.Fields.PriorityOrder")]
        public int PriorityOrder { get; set; }

        [NopResourceDisplayName("LDTracker.LotteryCategory.Fields.Active")]
        public bool Active { get; set; }
    }
}
