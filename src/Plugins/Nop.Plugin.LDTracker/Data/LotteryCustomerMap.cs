using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.LDTracker.Data
{
    public partial class LotteryCustomerMap : EntityTypeConfiguration<LotteryCustomer>
    {
        public LotteryCustomerMap()
        {
            ToTable("LotteryCustomer");
            HasKey(lc => lc.Id);

            Property(lc => lc.Name).IsRequired().HasMaxLength(100);
            Property(lc => lc.FullName).IsOptional().HasMaxLength(200);
            Property(lc => lc.Email).IsOptional();
            Property(lc => lc.MobilePhone).IsRequired().HasMaxLength(20);
            Property(lc => lc.Note).IsOptional().HasMaxLength(1000);
            Property(lc => lc.Address).IsOptional().HasMaxLength(1000);
            Property(lc => lc.Active).IsRequired();
            Property(lc => lc.Deleted).IsRequired();
            Property(lc => lc.CreateDate).IsRequired();
        }
    }
}
