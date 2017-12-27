using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Catalog;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Order
{
    public class StockItem
    {
        public int Id { get; set; }

        [DisplayName("تعداد")]
        [Required(ErrorMessage = "تعداد را وارد کنید")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "مبلغ را وارد کنید")]
        [DisplayName("قمیمت")]
        public int Price { get; set; }

        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
    }
}
