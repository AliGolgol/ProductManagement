using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Catalog;

namespace Repository.Data.Mapping.Catalog
{
    public class ProductCatConfig:EntityTypeConfiguration<ProductCat>
    {
        public ProductCatConfig()
        {
            this.HasRequired(p => p.ProductCategory)
                .WithMany(pc => pc.ProductCats)
                .HasForeignKey(p => p.ProductCategoryId);

            this.HasRequired(p => p.Product)
                .WithMany()
                .HasForeignKey(pc => pc.ProductId);
        }
    }
}
