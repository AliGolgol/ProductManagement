using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Catalog;

namespace Repository.DataLayer.Mapping.Catalog
{
    public class ManufacturerConfig:EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(p => p.Address).HasMaxLength(1000);
        }
    }
}
