using Repository.DomainModel.Order;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Order
{
    public class InvoiceConfig:EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfig()
        {
            this.HasKey(x => x.Id);

            this.Property(p => p.Description)
                .HasMaxLength(500);

            this.HasOptional(x => x.Period)
                .WithMany()
                .HasForeignKey(x => x.PeriedId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.ApplicattionUser)
                .WithMany()
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.PaymentType)
                .WithMany()
                .HasForeignKey(x => x.PaymentTypeId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.BillType)
                .WithMany()
                .HasForeignKey(x => x.BillTypeId)
                .WillCascadeOnDelete(false);
        }        
    }
}
