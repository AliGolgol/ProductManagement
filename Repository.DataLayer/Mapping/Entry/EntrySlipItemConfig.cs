using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Entry;

namespace Repository.DataLayer.Mapping.Entry
{
    public class EntrySlipItemConfig:EntityTypeConfiguration<EntrySlipItem>
    {
        public EntrySlipItemConfig()
        {
            this.HasKey(pk => pk.Id);

            this.Property(p => p.Description).HasMaxLength(500);

            this.HasRequired(x => x.EntrySlip)
                .WithMany()
                .HasForeignKey(fk => fk.EntrySlipId)
                .WillCascadeOnDelete();

            this.HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Repository)
                .WithMany(x=>x.EntrySlipItems)
                .HasForeignKey(x => x.RpsId)
                .WillCascadeOnDelete(false);

        }
    }
}
