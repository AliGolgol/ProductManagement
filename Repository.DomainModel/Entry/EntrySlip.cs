using Repository.DomainModel.AppUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Entry
{
    public class EntrySlip
    {
        public int Id  { get; set; }
        [DisplayName("تاریخ ثبت")]
        [Required(ErrorMessage = "تاریخ ثبت را وارد کنید")]
        public string CreatedDate { get; set; }
        [DisplayName("توضیحات")]       
        public string Description { get; set; }

        //who gives this?
        [DisplayName("تحویل دهنده")]
        public string DeliveryMan  { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public int? UserId { get; set; }

        public virtual EntrySlipType  EntrySlipType { get; set; }
        public int? ESTypeId { get; set; }

        public virtual Period.Period  Period { get; set; }
        public int? PeriodId { get; set; }

    }
}