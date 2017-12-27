using Repository.DomainModel.Common;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Common
{
    public class AboutConfig:EntityTypeConfiguration<About>
    {
        public AboutConfig()
        {
            this.HasKey(x => x.Id);

            this.Property(p => p.Address).HasMaxLength(500);
        }
    }
}
