using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Catalog;

namespace Repository.DomainModel.Order
{
    public class OrderItem
    {
        public int Id { get; set; }
        [DisplayName("تعداد")]
        public int Quantity { get; set; }
        [DisplayName("قمیمت")]
        public int Price { get; set; }

        public int ProductId { get; set; }
        public virtual Product  Products { get; set; }

        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
