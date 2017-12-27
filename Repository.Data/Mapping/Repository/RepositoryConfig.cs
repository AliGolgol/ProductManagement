using System.Data.Entity.ModelConfiguration;

namespace Repository.Data.Mapping.Repository
{
    public class RepositoryConfig:EntityTypeConfiguration<DomainModel.Repository.Repository>
    {
        public RepositoryConfig()
        {
            this.HasKey(r => r.Id);
            this.Ignore(ps => ps.PriceEstimate);
            this.HasOptional(r => r.Code);
            this.HasOptional(r=>r.RepositoryType)
                .WithMany()
                .HasForeignKey(rt=>rt.RepositoryTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
