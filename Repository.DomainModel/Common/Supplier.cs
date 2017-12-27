using Repository.DomainModel.Entry;
using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Common
{
    public class Supplier:Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="آدرس را وارد کنید")]
        [DisplayName("آدرس")]
        public string Address { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<BuySlip> BuySlips { get; set; }
    }
}
