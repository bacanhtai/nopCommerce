using Nop.Core;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Plugin.LDTracker.Domain;

namespace Nop.Plugin.LDTracker.Models
{
    public partial class LotteryModel : BaseNopEntityModel
    {
        public LotteryFull LotteryFull { get; set; }

        [NopResourceDisplayName("LDTracker.Lottery.Fields.DateRequest")]
        [UIHint("DateTimeNullable")]
        public DateTime? DateRequest { get; set; }
    }
}
