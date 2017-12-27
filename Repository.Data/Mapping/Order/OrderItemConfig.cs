using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Order;

namespace Repository.Data.Mapping.Order
{
    public class OrderItemConfig:EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfig()
        {
            this.HasRequired(o => o.Products)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            this.HasRequired(orderitem => orderitem.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);

           
        }
    }
}
