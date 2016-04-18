using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.LDTracker.Data
{
    public partial class LotteryCategoryMap : EntityTypeConfiguration<LotteryCategory>
    {
        public LotteryCategoryMap()
        {
            ToTable("LotteryCategory");
            HasKey(lc => lc.Id);

            Property(lc => lc.Name).IsRequired();
            Property(lc => lc.WinningUnit).IsRequired();
            Property(lc => lc.Price).IsRequired();
            Property(lc => lc.DuplicateWinning).IsRequired();
            Property(lc => lc.PriorityOrder).IsOptional();
            Property(lc => lc.Active).IsRequired();
        }
    }
}
