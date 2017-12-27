using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Common
{
    public class CustomerConfig:EntityTypeConfiguration<DomainModel.Common.Customer>
    {
        public CustomerConfig()
        {
            this.HasKey(x => x.Id);

            this.HasOptional(c => c.Address)
                .WithMany(c => c.Customers)
                .HasForeignKey(a => a.AddressId)
                .WillCascadeOnDelete(false);
        }
    }
}
