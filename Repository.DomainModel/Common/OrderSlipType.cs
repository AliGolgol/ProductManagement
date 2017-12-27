using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Common
{
    public class OrderSlipType
    {
        public int Id { get; set; }
        [DisplayName("انواع خروج")]
        public string  Name { get; set; }
    }
}
