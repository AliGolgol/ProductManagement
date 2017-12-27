using Repository.DomainModel.Entry;
using Repository.DomainModel.Order;
using System.Collections.Generic;
using System.ComponentModel;

namespace Repository.DomainModel.Period
{
    public class Period
    {
        public int Id { get; set; }
        [DisplayName("نام دوره")]
        public string Name { get; set; }

        [DisplayName("شروع")]
        public string StartDate { get; set; }

        [DisplayName("پایان")]
        public string EndDate { get; set; }

        public ICollection<Order.Order> Orders { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<EntrySlip> EntrySlips { get; set; }
    }
}
