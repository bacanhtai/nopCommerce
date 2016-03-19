using Nop.Core;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.LDTracker.Domain
{
    public partial class LotteryCategory : BaseEntity
    {
        public string Name { get; set; }
        public int WinningUnit { get; set; }
        public bool Active { get; set; }
        public int Price { get; set; }
        public int PriorityOrder { get; set; }
    }
}
