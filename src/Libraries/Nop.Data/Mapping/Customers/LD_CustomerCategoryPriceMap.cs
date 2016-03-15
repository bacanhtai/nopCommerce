using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public class LD_CustomerCategoryPriceMap : NopEntityTypeConfiguration<LD_CustomerCategoryPrice>
    {
        public LD_CustomerCategoryPriceMap()
        {
            this.ToTable("LD_CustomerCategoryPrice");
            this.HasKey(cc => cc.Id);
            this.HasRequired(cc => cc.Customer)
                .WithMany()
                .HasForeignKey(cc => cc.CustomerId);

            this.HasRequired(cc => cc.Category)
                .WithMany()
                .HasForeignKey(cc => cc.CategoryId);
        }
    }
}
