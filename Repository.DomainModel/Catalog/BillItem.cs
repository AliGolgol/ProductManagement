using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Catalog
{
    public class BillItem
    {
        public int Id { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [Required(ErrorMessage="تعداد را وارد کنید")]
        [DisplayName("تعداد")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "قیمت را وارد کنید")]
        [DisplayName("قیمت")]
        public int Price { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual Bill  Bill { get; set; }
        public int  BillId { get; set; }

    }
}
