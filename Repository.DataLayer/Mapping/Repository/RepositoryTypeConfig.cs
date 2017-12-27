using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Repository;

namespace Repository.DataLayer.Mapping.Repository
{
    public class RepositoryTypeConfig:EntityTypeConfiguration<RepositoryType>
    {
        public RepositoryTypeConfig()
        {
            this.HasKey(rt => rt.Id);
            
        }
    }
}
