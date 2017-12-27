using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Catalog;

namespace Repository.Data.Mapping.Catalog
{
    public class ManufacturerConfig:EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerConfig()
        {
            this.HasKey(x => x.Id);
            Property(p => p.Address).HasMaxLength(500);
        }
    }
}
