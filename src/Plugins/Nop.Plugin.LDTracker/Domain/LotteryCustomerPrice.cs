using Nop.Core;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.LDTracker.Domain
{
    public partial class LotteryCustomerPrice : BaseEntity
    {                 
        public int CategoryId { get; set; }
                      
        public int CustomerId { get; set; }
                                 
        public int Price { get; set; }
                      
        public int WinningUnit { get; set; }

        public bool Active { get; set; }

        public DateTime BeginUsedDate { get; set; }

        public virtual LotteryCategory Category { get; set; }

        public virtual LotteryCustomer Customer { get; set; }
    }
}
