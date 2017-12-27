using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Repository.DomainModel.Entry;
using Repository.DomainModel.Common;

namespace Repository.DomainModel.Order
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage="")]
        [DisplayName("تاریخ")]
        public string CreatedDate { get; set; }

        [Required(ErrorMessage = "")]
        [DisplayName("تاریخ پایان")]
        public string UpdatedDate { get; set; }

        public virtual Unit Unit { get; set; }
        public int? UnitId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public virtual Period.Period Period { get; set; }
        public int? PeriodId { get; set; }
        
    }
}