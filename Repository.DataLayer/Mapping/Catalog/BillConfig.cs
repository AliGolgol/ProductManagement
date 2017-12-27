using Repository.DomainModel.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Catalog
{
    public class BillConfig:EntityTypeConfiguration<Bill>
    {
        public BillConfig()
        {
            this.HasKey(x => x.Id);
            this.Property(p => p.Description)
                .HasMaxLength(500);

            this.HasOptional(x => x.BillType)
                .WithMany()
                .HasForeignKey(x => x.BiilTypeId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.PaymentType)
                .WithMany()
                .HasForeignKey(x => x.PaymentTypeId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.ApplicattionUser)
                .WithMany()
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.Period)
                .WithMany()
                .HasForeignKey(x => x.PeriedId)
                .WillCascadeOnDelete(false);

        }
    }
}
