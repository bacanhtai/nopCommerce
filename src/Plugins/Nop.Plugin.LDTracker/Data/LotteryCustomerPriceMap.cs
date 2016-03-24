using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.LDTracker.Data
{
    public partial class LotteryCustomerPriceMap : EntityTypeConfiguration<LotteryCustomerPrice>
    {
        public LotteryCustomerPriceMap()
        {
            ToTable("CategoryPrice_Customer_Mapping");
            HasKey(lc => lc.Id);

            this.HasRequired(cc => cc.Category)
                .WithMany()
                .HasForeignKey(cc => cc.CategoryId);

            this.HasRequired(cc => cc.Customer)
                .WithMany()
                .HasForeignKey(cc => cc.CustomerId);

            Property(lc => lc.Price).IsRequired();
            Property(lc => lc.WinningUnit).IsRequired();
            Property(lc => lc.Active).IsRequired();
            Property(lc => lc.BeginUsedDate).IsRequired();
        }
    }
}
