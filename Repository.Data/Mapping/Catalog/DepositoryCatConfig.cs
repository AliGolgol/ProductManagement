using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Catalog;

namespace Repository.Data.Mapping.Catalog
{
    public class DepositoryCatConfig:EntityTypeConfiguration<DepositoryCategory>
    {
        public DepositoryCatConfig()
        {
            this.HasKey(x => x.Id);
            HasOptional(p=>p.ParentCategory)
                .WithMany()
                .HasForeignKey(p=>p.ParentCategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
