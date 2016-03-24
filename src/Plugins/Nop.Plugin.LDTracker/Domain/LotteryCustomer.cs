using Nop.Core;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.LDTracker.Domain
{
    public partial class LotteryCustomer : BaseEntity
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string Note { get; set; }

        public string Address { get; set; }
        
        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
