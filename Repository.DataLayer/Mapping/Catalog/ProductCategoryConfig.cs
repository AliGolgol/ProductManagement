using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Catalog;

namespace Repository.DataLayer.Mapping.Catalog
{
    public class ProductCategoryConfig:EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfig()
        {
            this.HasKey(x => x.Id);
          
            this.HasOptional(p=>p.Parent)
                .WithMany()
                .HasForeignKey(p=>p.ParentId)
                .WillCascadeOnDelete(false);

            this.Property(p => p.MinimumBalance).IsOptional();

            this.Property(p => p.PackageCount).IsOptional();

            this.Property(p => p.Fee).IsOptional();
        }
        
    }
}
