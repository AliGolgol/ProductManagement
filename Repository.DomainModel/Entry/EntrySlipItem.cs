using Repository.DomainModel.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Entry
{
    public class EntrySlipItem
    {
        public int Id { get; set; }
        [DisplayName("تعداد")]
        [Required(ErrorMessage = "تعداد را وارد کنید")]
        public int Quantity { get; set; }
        [DisplayName("مبلغ")]
        [Required(ErrorMessage = "مبلغ را وارد کنید")]
        public int Price { get; set; }
        [DisplayName("توضیحات")]
        public string Description  { get; set; }

        public virtual EntrySlip  EntrySlip { get; set; }
        public int EntrySlipId { get; set; }
        public virtual Repository.Repository Repository { get; set; }
        [DisplayName("انبار")]
        public int RpsId { get; set; }
        public virtual Product Product { get; set; }
        [DisplayName("نام کالا")]
        public int ProductId { get; set; }

    }
}
