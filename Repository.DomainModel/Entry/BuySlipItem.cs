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
    public class BuySlipItem
    {
        public int Id { get; set; }
        [DisplayName("مبلغ کلا")]
        [Required(ErrorMessage = "مبلغ را وارد کنید")]
        public int Price { get; set; }
        [DisplayName("تعداد")]
        [Required(ErrorMessage = "تعداد را وارد کنید")]
        public int Quantity { get; set; }
       
        [DisplayName("توضیحات")]
        public string  Description { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual Repository.Repository  Repository { get; set; }
        public int RepositoryId { get; set; }

        public virtual BuySlip BuySlip { get; set; }
        public int BuySlipId { get; set; }


    }
}
