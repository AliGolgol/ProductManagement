using Repository.DomainModel.Entry;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Entry
{
    public class BuySlipItemConfig : EntityTypeConfiguration<BuySlipItem>
    {
        public BuySlipItemConfig()
        {
            this.HasKey(x => x.Id);

            this.Property(p => p.Description).HasMaxLength(500);

            this.HasRequired(x => x.Repository)
                .WithMany(x => x.BuySlipItems)
                .HasForeignKey(x => x.RepositoryId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.BuySlip)
                .WithMany(x => x.BuySlipItems)
                .HasForeignKey(x => x.BuySlipId)
                .WillCascadeOnDelete();

            this.HasRequired(x => x.Product)
                .WithMany(x => x.BuySlipItems)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);


        }
    }
}
