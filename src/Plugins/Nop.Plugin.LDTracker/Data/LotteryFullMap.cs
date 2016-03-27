using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.LDTracker.Data
{
    public partial class LotteryFullMap : EntityTypeConfiguration<LotteryFull>
    {
        public LotteryFullMap()
        {
            ToTable("LotteryFull");
            HasKey(lc => lc.Id);

            Property(lc => lc.item1).IsRequired();
            Property(lc => lc.item2).IsRequired();
            Property(lc => lc.item3).IsRequired();
            Property(lc => lc.item4).IsRequired();
            Property(lc => lc.item5).IsRequired();
            Property(lc => lc.item6).IsRequired();
            Property(lc => lc.item7).IsRequired();
            Property(lc => lc.item8).IsRequired();
            Property(lc => lc.item9).IsRequired();
            Property(lc => lc.item10).IsRequired();
            Property(lc => lc.item11).IsRequired();
            Property(lc => lc.item12).IsRequired();
            Property(lc => lc.item13).IsRequired();
            Property(lc => lc.item14).IsRequired();
            Property(lc => lc.item15).IsRequired();
            Property(lc => lc.item16).IsRequired();
            Property(lc => lc.item17).IsRequired();
            Property(lc => lc.item18).IsRequired();
            Property(lc => lc.item19).IsRequired();
            Property(lc => lc.item20).IsRequired();
            Property(lc => lc.item21).IsRequired();
            Property(lc => lc.item22).IsRequired();
            Property(lc => lc.item23).IsRequired();
            Property(lc => lc.item24).IsRequired();
            Property(lc => lc.item25).IsRequired();
            Property(lc => lc.item26).IsRequired();
            Property(lc => lc.item27).IsRequired();
            Property(lc => lc.LotteryDate).IsRequired();
            Property(lc => lc.LotteryType).IsRequired();
        }
    }
}
