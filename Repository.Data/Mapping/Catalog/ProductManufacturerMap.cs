using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Catalog;

namespace Repository.Data.Mapping.Catalog
{
    public class ProductManufacturerMap:EntityTypeConfiguration<ProductManufacturer>
    {
        public ProductManufacturerMap()
        {
            this.HasRequired(pm => pm.Product)
                .WithMany(p => p.ProductManufacturers)
                .HasForeignKey(pm => pm.ProductId);

            HasRequired(pm => pm.Manufacturer)
                .WithMany()
                .HasForeignKey(m => m.ManufacturerId);
        }
    }
}
