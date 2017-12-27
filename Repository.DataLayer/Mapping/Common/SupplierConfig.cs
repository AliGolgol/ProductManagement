using Repository.DomainModel.Common;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Common
{
    public class SupplierConfig:EntityTypeConfiguration<Supplier>
    {
        public SupplierConfig()
        {
            this.HasKey(s => s.Id);
            this.Property(p => p.Description).HasMaxLength(1000);
        }
    }
}
