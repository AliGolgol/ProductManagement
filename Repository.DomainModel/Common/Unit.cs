using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Order;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Common
{
    public class Unit
    {
        public int Id { get; set; }
        [DisplayName("نام واحد درخواست کننده")]
        [Required(ErrorMessage ="نام را وارد کنید")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        public ICollection<Order.Order> Orders { get; set; }
    }
}
