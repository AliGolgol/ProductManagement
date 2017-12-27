using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Repository
{
    public class RepositoryConfig:EntityTypeConfiguration<DomainModel.Repository.Repository>
    {
        public RepositoryConfig()
        {
            this.HasKey(r => r.Id);

            this.HasOptional(r=>r.RepositoryType)
                .WithMany()
                .HasForeignKey(rt=>rt.RepositoryTypeId)
                .WillCascadeOnDelete(false);

           
            this.Ignore(x => x.PriceEstimate);
            //this.HasOptional(p => p.Parent)
            //    .WithMany()
            //    .HasForeignKey(x => x.ParentId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
