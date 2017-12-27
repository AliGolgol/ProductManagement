using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Catalog;

namespace Repository.Data.Mapping.Catalog
{
    public class ProductCategoryConfig:EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfig()
        {
            this.HasKey(x => x.Id);
            HasOptional(p=>p.Parent)
                .WithMany()
                .HasForeignKey(p=>p.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
