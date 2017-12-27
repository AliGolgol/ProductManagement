using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Catalog;

namespace Repository.DataLayer.Mapping.Catalog
{
    public class ProductConfig:EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            this.HasKey(p => p.Id);

            this.HasOptional(m => m.Manufacturer)
                .WithMany()
                .HasForeignKey(m => m.ManufacturerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(pc => pc.ProductCategory)
                .WithMany(p=>p.Products)
                .HasForeignKey(pc => pc.ProductCategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
