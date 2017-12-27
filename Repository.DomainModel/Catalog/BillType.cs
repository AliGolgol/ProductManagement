using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Catalog
{
    public class BillType
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="لطفا نام را وارد کنید")]
        [DisplayName("نام")]
        public string  Name { get; set; }

        [DisplayName("توضیحات")]
        public string  Description { get; set; }

        [DisplayName("کسر از موجودی؟")]
        public bool  IsRemoval { get; set; }

        public ICollection<Bill> Bills { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
