using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Entry
{
    public class EntrySlipType
    {
        public int Id { get; set; }
        [DisplayName("انواع ورود کالا")]
        [Required(ErrorMessage = "نوع ورود را وارد کنید")]
        public string  Name { get; set; }

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<EntrySlip> EntrySlips { get; set; }
        public ICollection<BuySlip> BuySlips { get; set; }
    }
}
