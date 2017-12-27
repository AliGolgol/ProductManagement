using Repository.DomainModel.Order;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Order
{
    public class InvoiceItemConfig:EntityTypeConfiguration<InvoiceItem>
    {
        public InvoiceItemConfig()
        {
            this.HasKey(x => x.Id);

            this.Property(p => p.Description)
                .HasMaxLength(500);

            this.HasRequired(x => x.Invoice)
                .WithMany(x => x.InvoicesItems)
                .HasForeignKey(x => x.InvoiceId)
                .WillCascadeOnDelete();

            this.HasRequired(x => x.Product)
                .WithMany(x => x.InvoiceItems)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.Repository)
                .WithMany()
                .HasForeignKey(x => x.RepositoryId)
                .WillCascadeOnDelete(false);

        }
    }
}
