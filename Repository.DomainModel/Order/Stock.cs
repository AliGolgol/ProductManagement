using System.Collections.Generic;
using System.ComponentModel;
using Repository.DomainModel.AppUser;
using Repository.DomainModel.Entry;
using Repository.DomainModel.Common;

namespace Repository.DomainModel.Order
{
    public class Stock
    {
        public int Id { get; set; }

        [DisplayName("تاریخ")]
        public string CreatedDate { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        public bool Editable { get; set; }
        public ICollection<StockItem> StockItems { get; set; }

        public virtual Period.Period Period { get; set; }
        public int PeriodId { get; set; }

        
        public virtual Supplier Supplier { get; set; }
        [DisplayName("تامین کننده")]
        public int? SupplierId { get; set; }


        public virtual EntrySlipType EntrySlipType { get; set; }
        [DisplayName("انواع ورود کالا")]
        public int? EntrySlipTypeId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public int? ApplicationUserId { get; set; }
    }
}
